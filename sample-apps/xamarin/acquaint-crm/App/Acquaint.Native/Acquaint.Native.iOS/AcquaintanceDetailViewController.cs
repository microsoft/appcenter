using System;
using Acquaint.Abstractions;
using Acquaint.Models;
using Acquaint.Util;
using CoreAnimation;
using CoreGraphics;
using CoreLocation;
using FFImageLoading;
using FFImageLoading.Transformations;
using MapKit;
using Microsoft.Practices.ServiceLocation;
using ObjCRuntime;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Messaging;
using UIKit;

namespace Acquaint.Native.iOS
{
	/// <summary>
	/// Acquaintance detail view controller. The layout for this view controller is defined almost entirely in Main.storyboard.
	/// </summary>
	public partial class AcquaintanceDetailViewController : UIViewController
	{
		/// <summary>
		/// The data source.
		/// </summary>
		IDataSource<Acquaintance> _DataSource;

		public Acquaintance Acquaintance { get; set; }

		UIBarButtonItem DeleteBarButtonItem;

		readonly CLGeocoder _Geocoder;

		// This constructor signature is required, for marshalling between the managed and native instances of this class.
		public AcquaintanceDetailViewController(IntPtr handle) : base(handle)
		{
			_DataSource = ServiceLocator.Current.GetInstance<IDataSource<Acquaintance>>();
			_Geocoder = new CLGeocoder();
		}

		// This overridden method will be called after the AcquaintanceDetailViewController has been instantiated and loaded into memory,
		// but before the view hierarchy is rendered to the device screen.
		// The "async" keyword is added here to the override in order to allow other awaited async method calls inside the override to be called ascynchronously.
		public override async void ViewWillAppear(bool animated)
		{
			if (Acquaintance != null)
			{
				// set the title and label text properties
				Title = Acquaintance.DisplayName;
				CompanyNameLabel.Text = Acquaintance.Company;
				JobTitleLabel.Text = Acquaintance.JobTitle;
				StreetLabel.Text = Acquaintance.Street;
				CityLabel.Text = Acquaintance.City;
				StateAndPostalLabel.Text = Acquaintance.StatePostal;
				PhoneLabel.Text = Acquaintance.Phone;
				EmailLabel.Text = Acquaintance.Email;

				// Set image views for user actions.
				// The action for getting navigation directions is setup further down in this method, after the geocoding occurs.
				SetupSendMessageAction();
				SetupDialNumberAction();
				SetupSendEmailAction();

				 // use FFImageLoading library to asynchronously:
				await ImageService
					.Instance
					.LoadUrl(Acquaintance.PhotoUrl, TimeSpan.FromHours(Settings.ImageCacheDurationHours)) 					// get the image from a URL
					.LoadingPlaceholder("placeholderProfileImage.png") 	// specify a placeholder image
					.Transform(new CircleTransformation()) 				// transform the image to a circle
					.Error(e => System.Diagnostics.Debug.WriteLine(e.Message))
					.IntoAsync(ProfilePhotoImageView); 					// load the image into the UIImageView

				try
				{
					// asynchronously geocode the address
					var locations = await _Geocoder.GeocodeAddressAsync(Acquaintance.AddressString);

					// if we have at least one location
					if (locations != null && locations.Length > 0)
					{
						var coord = locations[0].Location.Coordinate;

						var span = new MKCoordinateSpan(MilesToLatitudeDegrees(20), MilesToLongitudeDegrees(20, coord.Latitude));

						// set the region that the map should display
						MapView.Region = new MKCoordinateRegion(coord, span);

						// create a new pin for the map
						var pin = new MKPointAnnotation()
						{
							Title = Acquaintance.DisplayName,
							Coordinate = new CLLocationCoordinate2D()
							{
								Latitude = coord.Latitude,
								Longitude = coord.Longitude
							}
						};

						// add the pin to the map
						MapView.AddAnnotation(pin);

						// add a top border to the MapView
						MapView.Layer.AddSublayer(new CALayer()
						{
							BackgroundColor = UIColor.LightGray.CGColor,
							Frame = new CGRect(0, 0, MapView.Frame.Width, 1)
						});

						// setup fhe action for getting navigation directions
						SetupGetDirectionsAction(coord.Latitude, coord.Longitude);
					}
				}
				catch
				{
					DisplayErrorAlertView("Geocoding Error", "Please make sure the address is valid and that you have a network connection.");
				}
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// override the back button text for AcquaintanceEditViewController (the navigated-to view controller)
			NavigationItem.BackBarButtonItem = new UIBarButtonItem("Details", UIBarButtonItemStyle.Plain, null);

			DeleteBarButtonItem = NavigationItem.RightBarButtonItems[1];

			DeleteBarButtonItem.Clicked += DeleteBarButtonItemClicked;
		}

		void DeleteBarButtonItemClicked(object sender, EventArgs ea)
		{
			UIAlertController alert =
				UIAlertController.Create(
					"Delete?",
					$"Are you sure you want to delete {Acquaintance.FirstName} {Acquaintance.LastName}?",
					UIAlertControllerStyle.Alert);

			// cancel button
			alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

			// delete button
			alert.AddAction(UIAlertAction.Create("Delete", UIAlertActionStyle.Destructive, async (action) => {
				if (action != null)
				{
					await _DataSource.RemoveItem(Acquaintance);

					NavigationController.PopViewController(true);
				}
			}));

			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			// get the destination viewcontroller from the segue
			var acquaintanceEditViewController = segue.DestinationViewController as AcquaintanceEditViewController;

			acquaintanceEditViewController.Acquaintance = Acquaintance;
		}

		void SetupGetDirectionsAction(double lat, double lon)
		{
			GetDirectionsImageView.Image = UIImage.FromBundle("directions");

			// UIImageView doesn't accept touch events by default, so we have to explcitly enable user interaction
			GetDirectionsImageView.UserInteractionEnabled = true;

			GetDirectionsImageView.AddGestureRecognizer(new UITapGestureRecognizer(async () => {
				// we're using the External Maps plugin from James Montemagno here (included as a NuGet)
				await CrossExternalMaps.Current.NavigateTo(Acquaintance.DisplayName, lat, lon, NavigationType.Driving);
			}));
		}

		void SetupSendMessageAction()
		{
			SendMessageImageView.Image = UIImage.FromBundle("message");

			// UIImageView doesn't accept touch events by default, so we have to explcitly enable user interaction
			SendMessageImageView.UserInteractionEnabled = true;

			SendMessageImageView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
				// we're using the Messaging plugin from Carel Lotz here (included as a NuGet)
				var smsTask = MessagingPlugin.SmsMessenger;
				if (smsTask.CanSendSms && IsRealDevice)
					smsTask.SendSms(Acquaintance.Phone, "");
				else
					DisplaySimulatorNotSupportedErrorAlertView("Messaging is not supported in the iOS simulator.");
			}));
		}

		void SetupDialNumberAction()
		{
			DialNumberImageView.Image = UIImage.FromBundle("phone");

			// UIImageView doesn't accept touch events by default, so we have to explcitly enable user interaction
			DialNumberImageView.UserInteractionEnabled = true;

			DialNumberImageView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
				// we're using the Messaging plugin from Carel Lotz here (included as a NuGet)
				var phoneCallTask = MessagingPlugin.PhoneDialer;
				if (phoneCallTask.CanMakePhoneCall && IsRealDevice)
					phoneCallTask.MakePhoneCall(Acquaintance.Phone);
				else
					DisplaySimulatorNotSupportedErrorAlertView("Phone calls are not supported in the iOS simulator.");
			}));
		}

		void SetupSendEmailAction()
		{
			SendEmailImageView.Image = UIImage.FromBundle("email");

			// UIImageView doesn't accept touch events by default, so we have to explcitly enable user interaction
			SendEmailImageView.UserInteractionEnabled = true;

			SendEmailImageView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
				// we're using the Messaging plugin from Carel Lotz here (included as a NuGet)
				var emailTask = MessagingPlugin.EmailMessenger;
				if (emailTask.CanSendEmail && IsRealDevice)
					emailTask.SendEmail(Acquaintance.Email, "");
				else
					DisplaySimulatorNotSupportedErrorAlertView("Email composition is not supported in the iOS simulator.");
			}));
		}

		void DisplaySimulatorNotSupportedErrorAlertView(string message)
		{
			DisplayErrorAlertView("Simulator Not Supported", message);
		}

		void DisplayErrorAlertView(string title, string message)
		{
			//Create Alert
			var okAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			//Add Action
			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

			// Present Alert
			PresentViewController(okAlertController, true, null);
		}

		/// <summary>
		/// Convert miles to latitude degrees.
		/// </summary>
		/// <returns>The latitude in degrees.</returns>
		/// <param name="miles">Miles.</param>
		static double MilesToLatitudeDegrees(double miles)
		{
			const double earthRadius = 3960.0; // In miles. Wow!
			const double radiansToDegrees = 180.0 / Math.PI;
			return miles / earthRadius * radiansToDegrees;
		}

		/// <summary>
		/// Convert miles to longitude degrees.
		/// </summary>
		/// <returns>The longitude in degrees.</returns>
		/// <param name="miles">Miles.</param>
		/// <param name="atLatitude">At latitude.</param>
		static double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			const double earthRadius = 3960.0; // In miles. Wow!
			const double degreesToRadians = Math.PI / 180.0;
			const double radiansToDegrees = 180.0 / Math.PI;
			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
			return miles / radiusAtLatitude * radiansToDegrees;
		}

		bool IsRealDevice => Runtime.Arch == Arch.DEVICE;
	}
}

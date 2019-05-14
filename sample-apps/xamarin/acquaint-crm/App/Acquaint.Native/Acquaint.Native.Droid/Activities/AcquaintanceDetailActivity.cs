using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acquaint.Abstractions;
using Acquaint.Models;
using Acquaint.Util;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Support.V7.App;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.ServiceLocation;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Messaging;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Acquaint.Native.Droid
{
	/// <summary>
	/// Acquaintance detail activity.
	/// </summary>
	[Activity]
	public class AcquaintanceDetailActivity : AppCompatActivity, IOnMapReadyCallback
	{
		IDataSource<Acquaintance> _DataSource;
		Acquaintance _Acquaintance;
		string _AcquaintanceId;
		ImageView _GetDirectionsActionImageView;
		LatLng _GeocodedLocation;

		View _MainLayout;
		Bundle _SavedInstanceState;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_SavedInstanceState = savedInstanceState;

			_MainLayout = LayoutInflater.Inflate(Resource.Layout.AcquaintanceDetail, null);

			SetContentView(_MainLayout);

			SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));

			Title = SupportActionBar.Title = "";

			// ensure that the system bar color gets drawn
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			// enable the back button in the action bar
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);
		}

		protected override async void OnResume()
		{
			base.OnResume();

			_DataSource = ServiceLocator.Current.GetInstance<IDataSource<Acquaintance>>();

			// extract the acquaintance id from the intent
			_AcquaintanceId = Intent.GetStringExtra(GetString(Resource.String.acquaintanceDetailIntentKey));

			// fetch the acquaintance based on the id
			_Acquaintance = await _DataSource.GetItem(_AcquaintanceId);

			// set the activity title and action bar title
			Title = SupportActionBar.Title = _Acquaintance.DisplayName;

			SetupViews();

			SetupAnimations();
		}

		void SetupViews()
		{
			// inflate the content layout
			var contentLayout = _MainLayout.FindViewById<LinearLayout>(Resource.Id.acquaintanceDetailContentLayout);

			contentLayout.InflateAndBindRemoteImageView(Resource.Id.profilePhotoImageView, _Acquaintance.SmallPhotoUrl);

			// inflate and set the name text view
			contentLayout.InflateAndBindTextView(Resource.Id.nameTextView, _Acquaintance.DisplayName);

			// inflate and set the company name text view
			contentLayout.InflateAndBindTextView(Resource.Id.companyTextView, _Acquaintance.Company);

			// inflate and set the job title text view
			contentLayout.InflateAndBindTextView(Resource.Id.jobTitleTextView, _Acquaintance.JobTitle);

			contentLayout.InflateAndBindTextView(Resource.Id.streetAddressTextView, _Acquaintance.Street);

			contentLayout.InflateAndBindTextView(Resource.Id.cityTextView, _Acquaintance.City);

			contentLayout.InflateAndBindTextView(Resource.Id.statePostalTextView, _Acquaintance.StatePostal);

			contentLayout.InflateAndBindTextView(Resource.Id.phoneTextView, _Acquaintance.Phone);

			contentLayout.InflateAndBindTextView(Resource.Id.emailTextView, _Acquaintance.Email);

			_GetDirectionsActionImageView = contentLayout.InflateAndBindLocalImageViewByResource(Resource.Id.getDirectionsActionImageView, Resource.Mipmap.directions);
			_GetDirectionsActionImageView.Visibility = ViewStates.Invisible;
			_GetDirectionsActionImageView.Click += async (sender, e) => {
				if (_GeocodedLocation != null)
					// we're using the External Maps plugin from James Montemagno here (included as a NuGet)
					await CrossExternalMaps.Current.NavigateTo(_Acquaintance.DisplayName, _GeocodedLocation.Latitude, _GeocodedLocation.Longitude, NavigationType.Driving);
			};

			var messageActionImageView = contentLayout.InflateAndBindLocalImageViewByResource(Resource.Id.messageActionImageView, Resource.Mipmap.message);
			messageActionImageView.Click += (sender, e) => {
				// we're using the Messaging plugin from Carel Lotz here (included as a NuGet)
				var smsTask = MessagingPlugin.SmsMessenger;
				if (smsTask.CanSendSms)
					smsTask.SendSms(_Acquaintance.Phone.SanitizePhoneNumber(), "");
			};

			var phoneActionImageView = contentLayout.InflateAndBindLocalImageViewByResource(Resource.Id.phoneActionImageView, Resource.Mipmap.phone);
			phoneActionImageView.Click += (sender, e) => {
				// we're using the Messaging plugin from Carel Lotz here (included as a NuGet)
				var phoneCallTask = MessagingPlugin.PhoneDialer;
				if (phoneCallTask.CanMakePhoneCall)
					phoneCallTask.MakePhoneCall(_Acquaintance.Phone.SanitizePhoneNumber());
			};

			var emailActionImageView = contentLayout.InflateAndBindLocalImageViewByResource(Resource.Id.emailActionImageView, Resource.Mipmap.email);
			emailActionImageView.Click += (sender, e) => {
				// we're using the Messaging plugin from Carel Lotz here (included as a NuGet)
				var emailTask = MessagingPlugin.EmailMessenger;
				if (emailTask.CanSendEmail)
					emailTask.SendEmail(_Acquaintance.Email, "");
			};

			// inflate the map view
			var mapview = FindViewById<MapView>(Resource.Id.map);

			// create the map view with the context
			mapview.OnCreate(_SavedInstanceState);

			// get the map, which calls the OnMapReady() method below (by virtue of the IOnMapReadyCallback interface that this class implements)
			mapview.GetMapAsync(this);
		}

		void SetupAnimations()
		{

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				var enterTransition = TransitionInflater.From(this).InflateTransition(Resource.Transition.acquaintanceDetailActivityEnter);

				Window.SharedElementEnterTransition = enterTransition;
			}
		}

		#region Implementations IOnMapReadyCallback

		public async void OnMapReady(GoogleMap googleMap)
		{
			// disable the compass on the map
			googleMap.UiSettings.CompassEnabled = false;

			// disable the my location button
			googleMap.UiSettings.MyLocationButtonEnabled = false;

			// disable the map toolbar
			googleMap.UiSettings.MapToolbarEnabled = false;

			// prevent tap gestures (this will automatically open the external map application, which we don't want in this case)
			googleMap.MapClick += (sender, e) => {
				// an empty delegate, to prevent click events
			};

			// attempt to get the lat and lon for the address
			_GeocodedLocation = await GetPositionAsync();

			if (_GeocodedLocation != null)
			{
				// because we now have coordinates, show the get directions action image view, and wire up its click handler
				_GetDirectionsActionImageView.Visibility = ViewStates.Visible;

				// initialze the map
				MapsInitializer.Initialize(this);

				// display the map region that contains the point. (the zoom level has been defined on the map layout in AcquaintanceDetail.axml)
				googleMap.MoveCamera(CameraUpdateFactory.NewLatLng(_GeocodedLocation));

				// create a new pin
				var marker = new MarkerOptions();

				// set the pin's position
				marker.SetPosition(new LatLng(_GeocodedLocation.Latitude, _GeocodedLocation.Longitude));

				// add the pin to the map
				googleMap.AddMarker(marker);
			}
		}

		#endregion

		async Task<LatLng> GetPositionAsync()
		{
			const string errorMessage = "Timed out waiting for response from server";

			IList<Address> addresses = new List<Address>();
			bool firstPassTimedOut = false;

			try
			{
				// asynchronously retrieve a geocoded location for the acqaintance's address
				addresses = await new Geocoder(this).GetFromLocationNameAsync(_Acquaintance.AddressString, 1);
			}
			catch (Exception ex)
			{
				if (ex.Message == errorMessage)
				{
					firstPassTimedOut = true;
					ShowGeocodingErrorAlert();
				}
			}

			// A quirk in the underlying Android geocoding API (not Xamarin.Android) sometimes prevents 
			// valid addresses from returning coordinates unless the address number is rounded to a multiple of ten.
			// So, if we don't get coordinates on the first pass, we try a second time with a rounded address number.
			if (addresses.Count > 0)
			{
				return new LatLng(addresses.First().Latitude, addresses.First().Longitude);
			}
			else if (!firstPassTimedOut && addresses.Count == 0 && AddressBeginsWithNumber(_Acquaintance.AddressString))
			{
				try
				{
					addresses = await new Geocoder(this).GetFromLocationNameAsync(GetAddressWithRoundedStreetNumber(_Acquaintance.AddressString), 1);
				}
				catch (Exception ex)
				{
					if (ex.Message == errorMessage)
					{
						ShowGeocodingErrorAlert();
					}
				}

				if (addresses.Count > 0)
				{
					return new LatLng(addresses.First().Latitude, addresses.First().Longitude);
				}
			}

			return null;
		}

		void ShowGeocodingErrorAlert()
		{
			// as long as this activity is not yet destroyed, show an alert indicating the gecooding error
			if (!IsDestroyed)
			{
				//set alert for executing the task
				var alert = new Android.App.AlertDialog.Builder(this);

				alert.SetTitle("Geocoding Error");

				alert.SetMessage("An error occurred while converting the street address to GPS coordinates.");

				alert.SetPositiveButton("OK", (senderAlert, args) => {
					// an empty delegate body, because we just want to close the dialog and not take any other action
				});

				//run the alert in UI thread to display in the screen
				RunOnUiThread(() => {
					alert.Show();
				});
			}
		}

		void ShowDeleteConfirmationAlert()
		{
			// as long as this activity is not yet destroyed, show an alert for confirming the delete action
			if (!IsDestroyed)
			{
				//set alert for executing the task
				var alert = new Android.App.AlertDialog.Builder(this);

				alert.SetTitle("Delete?");

				alert.SetMessage($"Are you sure you want to delete {_Acquaintance.FirstName} {_Acquaintance.LastName}?");

				alert.SetPositiveButton("Delete", async (senderAlert, args) => {
					await _DataSource.RemoveItem(_Acquaintance);
					OnBackPressed();
				});

				alert.SetNegativeButton("Cancel", (senderAlert, args) => {
					// an empty delegate body, because we just want to close the dialog and not take any other action
				});

				//run the alert in UI thread to display in the screen
				RunOnUiThread(() => {
					alert.Show();
				});
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.AcquaintanceDetailMenu, menu);

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item != null)
			{
				switch (item.ItemId)
				{
				case Android.Resource.Id.Home:
					// execute a back navigation
					OnBackPressed();
					break;
				case Resource.Id.acquaintanceEditButton:
					StartActivity(GetEditIntent());
					break;
				case Resource.Id.acquaintanceDeleteButton:
					ShowDeleteConfirmationAlert();
					break;
				}
			}

			return base.OnOptionsItemSelected(item);
		}

		Intent GetEditIntent()
		{
			// setup an intent
			var editIntent = new Intent(this, typeof(AquaintanceEditActivity));

			// Add some identifying item data to the intent. In this case, the id of the acquaintance for which we're about to display the detail screen.
			editIntent.PutExtra(Resources.GetString(Resource.String.acquaintanceDetailIntentKey), _Acquaintance.Id);

			return editIntent;
		}

		// determines if the address begins with a number
		static bool AddressBeginsWithNumber(string address)
		{
			return !String.IsNullOrWhiteSpace(address) && Char.IsDigit(address.ToCharArray().First());
		}

		// returns a street address with the number portion rounded to the closest tens place.
		static string GetAddressWithRoundedStreetNumber(string address)
		{
			var endingIndex = GetEndingIndexOfNumericPortionOfAddress(address);

			if (endingIndex == 0)
				return address;

			int originalNumber;

			Int32.TryParse(address.Substring(0, endingIndex + 1), out originalNumber);

			if (originalNumber == 0)
				return address;

			var roundedNumber = originalNumber.RoundToLowestHundreds();

			return address.Replace(originalNumber.ToString(), roundedNumber.ToString());
		}

		// finds the last position index of the street address number
		static int GetEndingIndexOfNumericPortionOfAddress(string address)
		{
			int endingIndex = 0;

			for (int i = 0; i < address.Length; i++)
			{
				if (Char.IsDigit(address[i]))
					endingIndex = i;
				else
					break;
			}

			return endingIndex;
		}
	}
}


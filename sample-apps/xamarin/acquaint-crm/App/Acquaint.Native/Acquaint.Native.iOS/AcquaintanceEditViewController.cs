using System;
using Acquaint.Abstractions;
using Acquaint.Data;
using Acquaint.Models;
using Microsoft.Practices.ServiceLocation;
using UIKit;

namespace Acquaint.Native.iOS
{
	public partial class AcquaintanceEditViewController : UITableViewController
	{
		/// <summary>
		/// The data source.
		/// </summary>
		IDataSource<Acquaintance> _DataSource;

		/// <summary>
		/// A flag indicating that we're editing a new Acquaintance.
		/// </summary>
		bool _IsNew;

		Acquaintance _Acquaintance;
		public Acquaintance Acquaintance
		{
			get { return _Acquaintance; }
			set
			{
				_Acquaintance = value;
				_IsNew |= _Acquaintance == null;
			}
		}

		public AcquaintanceEditViewController(IntPtr handle) : base(handle)
		{
			_DataSource = ServiceLocator.Current.GetInstance<IDataSource<Acquaintance>>();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			_FirstNameField.Text = _Acquaintance?.FirstName;
			_LastNameField.Text = _Acquaintance?.LastName;
			_CompanyNameField.Text = _Acquaintance?.Company;
			_JobTitleField.Text = _Acquaintance?.JobTitle;
			_PhoneNumberField.Text = _Acquaintance?.Phone;
			_EmailAddressField.Text = _Acquaintance?.Email;
			_StreetField.Text = _Acquaintance?.Street;
			_CityField.Text = _Acquaintance?.City;
			_StateField.Text = _Acquaintance?.State;
			_ZipField.Text = _Acquaintance?.PostalCode;

			NavigationItem.RightBarButtonItem.Clicked += async (sender, e) => {

				if (string.IsNullOrWhiteSpace(_FirstNameField.Text) || string.IsNullOrWhiteSpace(_LastNameField.Text))
				{
					UIAlertController alert = UIAlertController.Create("Invalid name!", "A acquaintance must have both a first and last name.", UIAlertControllerStyle.Alert);

					// cancel button
					alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

				}
				else if (!RequiredAddressFieldCombinationIsFilled)
				{
					UIAlertController alert = UIAlertController.Create("Invalid address!", "You must enter either a street, city, and state combination, or a postal code.", UIAlertControllerStyle.Alert);

					// cancel button
					alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
				}
				else {

					if (_Acquaintance == null)
						_Acquaintance = new Acquaintance();

					_Acquaintance.FirstName = _FirstNameField.Text;
					_Acquaintance.LastName = _LastNameField.Text;
					_Acquaintance.Company = _CompanyNameField.Text;
					_Acquaintance.JobTitle = _JobTitleField.Text;
					_Acquaintance.Phone = _PhoneNumberField.Text;
					_Acquaintance.Email = _EmailAddressField.Text;
					_Acquaintance.Street = _StreetField.Text;
					_Acquaintance.City = _CityField.Text;
					_Acquaintance.State = _StateField.Text;
					_Acquaintance.PostalCode = _ZipField.Text;


					if (_IsNew)
						await _DataSource.AddItem(_Acquaintance);
					else
						await _DataSource.UpdateItem(_Acquaintance);

					NavigationController.PopViewController(true);
				}
			};
		}

		bool RequiredAddressFieldCombinationIsFilled
		{
			get
			{
				if (String.IsNullOrWhiteSpace(AddressString))
				{
					return true;
				}
				if (!String.IsNullOrWhiteSpace(_StreetField.Text) && !String.IsNullOrWhiteSpace(_CityField.Text) && !String.IsNullOrWhiteSpace(_StateField.Text))
				{
					return true;
				}
				if (!String.IsNullOrWhiteSpace(_ZipField.Text) && (String.IsNullOrWhiteSpace(_StreetField.Text) || String.IsNullOrWhiteSpace(_CityField.Text) || String.IsNullOrWhiteSpace(_StateField.Text)))
				{
					return true;
				}

				return false;
			}
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		string AddressString
		{
			get
			{
				return string.Format(
					"{0} {1} {2} {3}",
					_StreetField.Text,
					!string.IsNullOrWhiteSpace(_CityField.Text) ? _CityField.Text + "," : "",
					_StateField.Text,
					_ZipField.Text);
			}
		}
	}
}



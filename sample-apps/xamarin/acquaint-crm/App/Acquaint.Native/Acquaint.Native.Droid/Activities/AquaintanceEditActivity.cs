using Acquaint.Abstractions;
using Acquaint.Models;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.ServiceLocation;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Acquaint.Native.Droid
{
	[Activity]
	public class AquaintanceEditActivity : AppCompatActivity
	{
		IDataSource<Acquaintance> _DataSource;
		Acquaintance _Acquaintance;

		bool _IsNewAcquaintance;

		View _MainLayout;
		View _ContentLayout;

		EditText _FirstNameField;
		EditText _LastNameField;
		EditText _CompanyField;
		EditText _JobTitleField;
		EditText _PhoneField;
		EditText _EmailField;
		EditText _StreetField;
		EditText _CityField;
		EditText _StateField;
		EditText _ZipField;

		protected override async void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_MainLayout = LayoutInflater.Inflate(Resource.Layout.AcquaintanceEdit, null);

			SetContentView(_MainLayout);

			SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));

			// ensure that the system bar color gets drawn
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			// enable the back button in the action bar
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);

			Title = SupportActionBar.Title = "";
		}

		protected override async void OnResume()
		{
			base.OnResume();

			// extract the acquaintance id from the intent
			var acquaintanceId = Intent.GetStringExtra(GetString(Resource.String.acquaintanceEditIntentKey));

			_DataSource = ServiceLocator.Current.GetInstance<IDataSource<Acquaintance>>();

			if (acquaintanceId == null)
			{
				_Acquaintance = new Acquaintance();
				_IsNewAcquaintance = true;
			}
			else
			{
				// fetch the acquaintance based on the id
				_Acquaintance = await _DataSource.GetItem(acquaintanceId);
			}

			SetupViews();
		}

		void SetupViews()
		{
			_ContentLayout = _MainLayout.FindViewById<LinearLayout>(Resource.Id.acquaintanceEditContentLayout);


			_ContentLayout.InflateAndBindTextView(Resource.Id.nameSectionTitleTextView, "Name");

			_ContentLayout.InflateAndBindTextView(Resource.Id.firstNameLabel, "First");
			_FirstNameField = _ContentLayout.InflateAndBindEditText(Resource.Id.firstNameField, _Acquaintance.FirstName);

			_ContentLayout.InflateAndBindTextView(Resource.Id.lastNameLabel, "Last");
			_LastNameField = _ContentLayout.InflateAndBindEditText(Resource.Id.lastNameField, _Acquaintance.LastName);


			_ContentLayout.InflateAndBindTextView(Resource.Id.employmentSectionTitleTextView, "Employment");

			_ContentLayout.InflateAndBindTextView(Resource.Id.companyLabel, "Company");
			_CompanyField = _ContentLayout.InflateAndBindEditText(Resource.Id.companyField, _Acquaintance.Company);

			_ContentLayout.InflateAndBindTextView(Resource.Id.jobTitleLabel, "Title");
			_JobTitleField = _ContentLayout.InflateAndBindEditText(Resource.Id.jobTitleField, _Acquaintance.JobTitle);


			_ContentLayout.InflateAndBindTextView(Resource.Id.contactSectionTitleTextView, "Contact");

			_ContentLayout.InflateAndBindTextView(Resource.Id.phoneNumberLabel, "Phone");
			_PhoneField = _ContentLayout.InflateAndBindEditText(Resource.Id.phoneNumberField, _Acquaintance.Phone);

			_ContentLayout.InflateAndBindTextView(Resource.Id.emailLabel, "Email");
			_EmailField = _ContentLayout.InflateAndBindEditText(Resource.Id.emailField, _Acquaintance.Email);


			_ContentLayout.InflateAndBindTextView(Resource.Id.addressSectionTitleTextView, "Address");

			_ContentLayout.InflateAndBindTextView(Resource.Id.streetLabel, "Street");
			_StreetField = _ContentLayout.InflateAndBindEditText(Resource.Id.streetField, _Acquaintance.Street);

			_ContentLayout.InflateAndBindTextView(Resource.Id.cityLabel, "City");
			_CityField = _ContentLayout.InflateAndBindEditText(Resource.Id.cityField, _Acquaintance.City);

			_ContentLayout.InflateAndBindTextView(Resource.Id.stateLabel, "State");
			_StateField = _ContentLayout.InflateAndBindEditText(Resource.Id.stateField, _Acquaintance.State);

			_ContentLayout.InflateAndBindTextView(Resource.Id.zipLabel, "ZIP");
			_ZipField = _ContentLayout.InflateAndBindEditText(Resource.Id.zipField, _Acquaintance.PostalCode);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.AcquaintanceEditMenu, menu);

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				OnBackPressed();
				break;
			case Resource.Id.acquaintanceSaveButton:
				Save();
				break;
			}

			return base.OnOptionsItemSelected(item);
		}

		void Save()
		{
			if (string.IsNullOrWhiteSpace(_FirstNameField.Text) || string.IsNullOrWhiteSpace(_LastNameField.Text))
			{
				//set alert for executing the task
				var alert = new Android.App.AlertDialog.Builder(this);

				alert.SetTitle("Invalid name!");

				alert.SetMessage("An acquaintance must have both a first and last name.");

				alert.SetNegativeButton("OK", (senderAlert, args) => {
					// an empty delegate body, because we just want to close the dialog and not take any other action
				});

				//run the alert in UI thread to display in the screen
				RunOnUiThread(() => {
					alert.Show();
				});

				return;
			}

			if (!RequiredAddressFieldCombinationIsFilled)
			{
				//set alert for executing the task
				var alert = new Android.App.AlertDialog.Builder(this);

				alert.SetTitle("Invalid address!");

				alert.SetMessage("You must enter either a street, city, and state combination, or a postal code.");

				alert.SetNegativeButton("OK", (senderAlert, args) => {
					// an empty delegate body, because we just want to close the dialog and not take any other action
				});

				//run the alert in UI thread to display in the screen
				RunOnUiThread(() => {
					alert.Show();
				});

				return;
			}

			_Acquaintance.FirstName = _FirstNameField.Text;
			_Acquaintance.LastName = _LastNameField.Text;
			_Acquaintance.Company = _CompanyField.Text;
			_Acquaintance.JobTitle = _JobTitleField.Text;
			_Acquaintance.Phone = _PhoneField.Text;
			_Acquaintance.Email = _EmailField.Text;
			_Acquaintance.Street = _StreetField.Text;
			_Acquaintance.City = _CityField.Text;
			_Acquaintance.State = _StateField.Text;
			_Acquaintance.PostalCode = _ZipField.Text;

			if (_IsNewAcquaintance)
			{
				_DataSource.AddItem(_Acquaintance);
			}
			else
			{
				_DataSource.UpdateItem(_Acquaintance);
			}

			OnBackPressed();
		}

		bool RequiredAddressFieldCombinationIsFilled
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(_StreetField.Text) && !string.IsNullOrWhiteSpace(_CityField.Text) && !string.IsNullOrWhiteSpace(_StateField.Text))
				{
					return true;
				}

				if (!string.IsNullOrWhiteSpace(_ZipField.Text) && string.IsNullOrWhiteSpace(_StreetField.Text) || string.IsNullOrWhiteSpace(_CityField.Text) || string.IsNullOrWhiteSpace(_StateField.Text))
				{
					return true;
				}

				if (string.IsNullOrWhiteSpace(AddressString))
				{
					return true;
				}

				return false;
			}
		}

		string AddressString => 
			string.Format(
				"{0} {1} {2} {3}",
				_StreetField.Text,
				!string.IsNullOrWhiteSpace(_CityField.Text) ? _CityField.Text + "," : "",
				_StateField.Text,
				_ZipField.Text);
	}
}


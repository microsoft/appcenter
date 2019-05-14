using System;
using System.Threading.Tasks;
using Acquaint.Util;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Cache;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Acquaint.Native.Droid
{
	[Activity(Label = "SettingsActivity")]
	public class SettingsActivity : AppCompatActivity
	{
		View _MainLayout;
		EditText _DataPartitionPhraseField;
		EditText _BackendUrlField;
		EditText _ImageCacheDurationField;
		Switch _ClearImageCacheSwitch;
		Switch _ResetToDefaultsSwitch;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_MainLayout = LayoutInflater.Inflate(Resource.Layout.Settings, null);

			// set the content view
			SetContentView(_MainLayout);

			// setup the action bar
			SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));

			// ensure that the system bar color gets drawn
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			// set the title of both the activity and the action bar
			Title = SupportActionBar.Title = "Settings";

			SetupViews();
		}

		void SetupViews()
		{
			// inflate the content layout
			var contentLayout = _MainLayout.FindViewById<LinearLayout>(Resource.Id.settingsContentLayout);

			contentLayout.InflateAndBindTextView(Resource.Id.settingsDataPartitionPhraseSectionTitleTextView);
			_DataPartitionPhraseField = contentLayout.InflateAndBindEditText(Resource.Id.settingsDataPartitionPhraseField, Settings.DataPartitionPhrase);

			contentLayout.InflateAndBindTextView(Resource.Id.settingsBackendUrlSectionTitleTextView);
			_BackendUrlField = contentLayout.InflateAndBindEditText(Resource.Id.settingsBackendUrlField, Settings.AzureAppServiceUrl);

			contentLayout.InflateAndBindTextView(Resource.Id.settingsImageCacheDurationSectionTitleTextView);
			_ImageCacheDurationField = contentLayout.InflateAndBindEditText(Resource.Id.settingsImageCacheDurationField, Settings.ImageCacheDurationHours.ToString());

			_ClearImageCacheSwitch = contentLayout.InflateAndBindSwitch(Resource.Id.settingsClearImageCacheSwitch, false);

			_ResetToDefaultsSwitch = contentLayout.InflateAndBindSwitch(Resource.Id.settingsResetToDefaultsSwitch, false);

			_ResetToDefaultsSwitch.CheckedChange += ResetToDefaultsSwitchCheckedChange;
		}

		void ResetToDefaultsSwitchCheckedChange(object sender, EventArgs e)
		{
			if (((Switch)sender).Checked)
				_ClearImageCacheSwitch.Checked = true;
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.SettingsMenu, menu);

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item != null)
			{
				switch (item.ItemId)
				{
				case Android.Resource.Id.Home:
					// execute a back navigation from the back button
					OnBackPressed();
					break;
				case Resource.Id.settingsCancelButton:
					// execute a back navigation from the cancel button
					OnBackPressed();
					break;
				case Resource.Id.settingsSaveButton:
					// save the changes
					Save();
					break;
				}
			}

			return base.OnOptionsItemSelected(item);
		}

		void Save()
		{
			// Start a new thread because the caller does not support async.
			// MAKE SURE TO CALL ALL UI METHODS WITH RunOnUiThread(), because we're now in a background thread.
			Task.Factory.StartNew(async () => {

				// if the reset to defaults has been requested
				if (_ResetToDefaultsSwitch.Checked)
				{
					// reset the settings
					Settings.ResetUserConfigurableSettingsToDefaults();

					// set image cache clear flag to true
					Settings.ClearImageCacheIsRequested = true;
				}
				// if image cache clear has been explicitly requested
				else if (_ClearImageCacheSwitch.Checked)
				{
					// set image cache clear flag to true
					Settings.ClearImageCacheIsRequested = true;
				}
				else
				{
					// Check the data partition id
					if (string.IsNullOrWhiteSpace(_DataPartitionPhraseField.Text))
					{
						// We're on a background thread, so make sure to set the hint text color with RunOnUiThread()
						RunOnUiThread(() => _DataPartitionPhraseField.SetHintTextColor(Color.Red));

						return;
					}

					// Check the backend service URL
					Uri testUri;
					if (!Uri.TryCreate(_BackendUrlField.Text, UriKind.Absolute, out testUri))
					{
						// as long as this activity is not yet destroyed, show an alert indicating that the URL is invalid
						if (!IsDestroyed)
						{
							//set alert for executing the task
							var alert = new Android.App.AlertDialog.Builder(this);

							alert.SetTitle("Invalid URL");

							alert.SetMessage("Please enter a valid URL");

							alert.SetPositiveButton("OK", (senderAlert, args) => {
								// an empty delegate body, because we just want to close the dialog and not take any other action
							});

							// We're on a background thread, so make sure to call alert.Show() with RunOnUiThread()
							RunOnUiThread(() => alert.Show());
						}

						return;
					}

					int localStoreResetConditions = 0;

					// if the backend service URL has changed, then we want to reset the local datastore
					if (Settings.AzureAppServiceUrl.ToLower() != _BackendUrlField.Text.ToLower())
						localStoreResetConditions++;

					// set the backend service URL
					Settings.AzureAppServiceUrl = _BackendUrlField.Text;

					// if the data partition phrase has changed, then we want to reset the local datastore
					if (Settings.DataPartitionPhrase.ToLower() != _DataPartitionPhraseField.Text.ToLower())
						localStoreResetConditions++;

					// set the data partition phrase
					Settings.DataPartitionPhrase = _DataPartitionPhraseField.Text;

					// if we've triggered at last one condition for local datastore, then set the flag in the Settings class
					if (localStoreResetConditions > 0)
						Settings.LocalDataResetIsRequested = true;

					// we're enforcing a minimum image cache duration
					int ImageCacheDurationValue = Settings.ImageCacheDurationHoursDefault;

					int.TryParse(_ImageCacheDurationField.Text, out ImageCacheDurationValue);

					if (ImageCacheDurationValue < Settings.ImageCacheDurationHoursDefault)
						_ImageCacheDurationField.Text = Settings.ImageCacheDurationHoursDefault.ToString();

					// if either the image cache sureation has changed or local datastore reset is being requested, then clear the image cache
					if (Settings.ImageCacheDurationHours != ImageCacheDurationValue || Settings.LocalDataResetIsRequested)
						Settings.ClearImageCacheIsRequested = true;

					// set the image cache duration
					Settings.ImageCacheDurationHours = ImageCacheDurationValue;
				}

				// if image cache reset has been requested
				if (Settings.ClearImageCacheIsRequested)
				{
					// clear image cache
					await ImageService
						.Instance
						.InvalidateCacheAsync(CacheType.All);
				}

				// We're on a background thread, so make sure to call OnBackPressed() with RunOnUiThread()
				RunOnUiThread(() => OnBackPressed());
			});
		}
	}
}


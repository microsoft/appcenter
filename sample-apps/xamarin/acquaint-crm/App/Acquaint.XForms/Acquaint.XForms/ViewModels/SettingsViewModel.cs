using System;
using System.Threading.Tasks;
using Acquaint.Util;
using FFImageLoading;
using FFImageLoading.Cache;
using FormsToolkit;
using Xamarin.Forms;

namespace Acquaint.XForms
{
	public class SettingsViewModel : BaseNavigationViewModel
	{
		public string AzureAppServiceUrl { get; set; }
		public string DataPartitionPhrase { get; set; }
		public int ImageCacheDurationHours { get; set; }
		public bool ClearImageCache { get; set; }

		bool _ResetToDefaults;
		public bool ResetToDefaults
		{
			get { return _ResetToDefaults; }
			set
			{
				SetProperty(ref _ResetToDefaults, value);
				// a reset to defaults implicitly resets the image cache as well
				if (value)
				{
					ClearImageCache = value; // if the data is being refreshed, we should clear the image cache as well
					OnPropertyChanged(nameof(ClearImageCache)); // notify that ClearImageCache has been updated
				}
			}
		}

		public SettingsViewModel()
		{
			// load values from Settings into local properties
			AzureAppServiceUrl = Settings.AzureAppServiceUrl;
			DataPartitionPhrase = Settings.DataPartitionPhrase;
			ImageCacheDurationHours = Settings.ImageCacheDurationHours;
		}

		Command _CancelCommand;

		public Command CancelCommand => _CancelCommand ?? (_CancelCommand = new Command(async () => await ExecuteCancelCommand()));

		async Task ExecuteCancelCommand()
		{
			await PopModalAsync();
		}

		Command _SaveCommand;

		public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(async () => await ExecuteSaveCommand()));

		async Task ExecuteSaveCommand()
		{
			// if the reset to defaults has been requested
			if (ResetToDefaults)
			{
				// reset the settings
				Settings.ResetUserConfigurableSettingsToDefaults();

				// set image cache clear flag to true
				Settings.ClearImageCacheIsRequested = true;
			}
			// if image cache clear has been explicitly requested
			else if (ClearImageCache)
			{
				// set image cache clear flag to true
				Settings.ClearImageCacheIsRequested = true;
			}
			else
			{
				// Check the data partition id
				if (string.IsNullOrWhiteSpace(DataPartitionPhrase))
				{
					// Publish to the MessagingService, indicating that the data partition phrase is not valid.
					// This message is subcribed to in the SettingsPage.xaml.cs code-behind.
					MessagingService.Current.SendMessage(MessageKeys.DataPartitionPhraseValidation);
					return;
				}

				// Check the backend service URL
				Uri testUri;
				if (!Uri.TryCreate(AzureAppServiceUrl, UriKind.Absolute, out testUri))
				{
					// Publish to the MessagingService, indicating that an alert should be shown.
					// This message is subcribed to in App.xaml.cs.
					MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
					{
						Title = "Invalid URL",
						Message = "Please enter a valid URL",
						Cancel = "OK"
					});

					return;
				}

				int localStoreResetConditions = 0;

				// if the backend service URL has changed, then we want to reset the local datastore
				if (Settings.AzureAppServiceUrl.ToLower() != AzureAppServiceUrl.ToLower())
					localStoreResetConditions++;

				// set the backend service URL
				Settings.AzureAppServiceUrl = AzureAppServiceUrl;

				// if the data partition phrase has changed, then we want to reset the local datastore
				if (Settings.DataPartitionPhrase.ToLower() != DataPartitionPhrase.ToLower())
					localStoreResetConditions++;

				// set the data partition phrase
				Settings.DataPartitionPhrase = DataPartitionPhrase;

				// if we've triggered at last one condition for local datastore, then set the flag in the Settings class
				if (localStoreResetConditions > 0)
					Settings.LocalDataResetIsRequested = true;

				// we're enforcing a minimum image cache duration of one hour
				if (ImageCacheDurationHours < Settings.ImageCacheDurationHoursDefault)
					ImageCacheDurationHours = Settings.ImageCacheDurationHoursDefault;

				// if either the image cache sureation has changed or local datastore reset is being requested, then clear the image cache
				if (Settings.ImageCacheDurationHours != ImageCacheDurationHours || Settings.LocalDataResetIsRequested)
					Settings.ClearImageCacheIsRequested = true;

				// set the image cache duration
				Settings.ImageCacheDurationHours = ImageCacheDurationHours;
			}

			// if image cache reset has been requested
			if (Settings.ClearImageCacheIsRequested)
			{
				// clear image cache
				await ImageService
					.Instance
					.InvalidateCacheAsync(CacheType.All);
			}

			// modally pop the page
			await PopModalAsync();
		}
	}
}


using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Acquaint.Util
{
	/// <summary>
	/// This class uses the Xamarin settings plugin (Plugins.Settings) to implement cross-platform storing of settings.
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get { return CrossSettings.Current; }
		}

		public static void ResetUserConfigurableSettingsToDefaults()
		{
			DataIsSeeded = DataIsSeededDefault;
			LocalDataResetIsRequested = true;
			AzureAppServiceUrl = AzureAppServiceUrlDefault;
			ImageCacheDurationHours = ImageCacheDurationHoursDefault;
		}

        public static bool IsUsingLocalDataSource => true;// DataPartitionPhrase == "UseLocalDataSource";

		public static event EventHandler OnDataPartitionPhraseChanged;

		/// <summary>
		/// Raises the data parition phrase changed event.
		/// </summary>
		/// <param name="e">E.</param>
		static void RaiseDataParitionPhraseChangedEvent(EventArgs e)
		{
			var handler = OnDataPartitionPhraseChanged;

			if (handler != null)
				handler(null, e);
		}

		#region user-configurable

		private const string AzureAppServiceUrlKey = "AzureAppServiceUrl_key";
		private static readonly string AzureAppServiceUrlDefault = "https://app-acquaint.azurewebsites.net";

		private const string DataPartitionPhraseKey = "DataPartitionPhrase_key";
		private static readonly string DataSeedPhraseDefault = "";

		private const string ImageCacheDurationHoursKey = "ImageCacheDurationHours_key";
		public static readonly int ImageCacheDurationHoursDefault = 1;

		public static string AzureAppServiceUrl
		{
			get { return AppSettings.GetValueOrDefault<string>(AzureAppServiceUrlKey, AzureAppServiceUrlDefault); }
			set { AppSettings.AddOrUpdateValue<string>(AzureAppServiceUrlKey, value); }
		}

		public static string DataPartitionPhrase
		{
			get { return AppSettings.GetValueOrDefault<string>(DataPartitionPhraseKey, DataSeedPhraseDefault); }
			set
			{ 
				AppSettings.AddOrUpdateValue<string>(DataPartitionPhraseKey, value);
				RaiseDataParitionPhraseChangedEvent(null);
			}
		}

		public static int ImageCacheDurationHours
		{
			get { return AppSettings.GetValueOrDefault<int>(ImageCacheDurationHoursKey, ImageCacheDurationHoursDefault); }
			set { AppSettings.AddOrUpdateValue<int>(ImageCacheDurationHoursKey, value); }
		}

		#endregion


		#region non-user-configurable

		private const string DataIsSeededKey = "DataIsSeeded_key";
		private static readonly bool DataIsSeededDefault = false;

		private const string LocalDataResetIsRequestedKey = "LocalDataResetIsRequested_key";
		private static readonly bool LocalDataResetIsRequestedDefault = false;

		private const string ClearImageCacheIsRequestedKey = "ClearImageCacheIsRequested_key";
		private static readonly bool ClearImageCacheIsRequestedDefault = false;

		private const string HockeyAppIdKey = "HockeyAppId_key";
		private static readonly string HockeyAppIdDefault = "11111111222222223333333344444444"; // This is just a placeholder value. Replace with your real HockeyApp App ID.

        private const string BingMapsKeyKey = "BingMapsKey_key";
        private static readonly string BingMapsKeyDefault = "UW0peICp3gljJyhqQKFZ~R3XF1I5BvWmWmkD4ujytTA~AoUOqpk2nJB-Wh7wH-9S-zaG-w6sygLitXugNOqm71wx_nc6WHIt6Lb29gyTU04X";

		public static bool LocalDataResetIsRequested
		{
			get { return AppSettings.GetValueOrDefault<bool>(LocalDataResetIsRequestedKey, LocalDataResetIsRequestedDefault); }
			set { AppSettings.AddOrUpdateValue<bool>(LocalDataResetIsRequestedKey, value); }
		}

		public static bool ClearImageCacheIsRequested
		{
			get { return AppSettings.GetValueOrDefault<bool>(ClearImageCacheIsRequestedKey, ClearImageCacheIsRequestedDefault); }
			set { AppSettings.AddOrUpdateValue<bool>(ClearImageCacheIsRequestedKey, value); }
		}

        public static bool DataIsSeeded
		{
			get { return AppSettings.GetValueOrDefault<bool>(DataIsSeededKey, DataIsSeededDefault); }
			set { AppSettings.AddOrUpdateValue<bool>(DataIsSeededKey, value); }
		}

		public static string HockeyAppId
		{
			get { return AppSettings.GetValueOrDefault<string>(HockeyAppIdKey, HockeyAppIdDefault); }
			set { AppSettings.AddOrUpdateValue<string>(HockeyAppIdKey, value); }
		}

        public static string BingMapsKey
        {
            get { return AppSettings.GetValueOrDefault<string>(BingMapsKeyKey, BingMapsKeyDefault); }
            set { AppSettings.AddOrUpdateValue<string>(BingMapsKeyKey, value); }
        }

		#endregion
	}
}


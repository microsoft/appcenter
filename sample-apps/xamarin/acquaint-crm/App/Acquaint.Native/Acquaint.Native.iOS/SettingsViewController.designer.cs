// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Acquaint.Native.iOS
{
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		UIKit.UITextField BackendUrlEntry { get; set; }

		[Outlet]
		UIKit.UISwitch ClearImageCacheSwitch { get; set; }

		[Outlet]
		UIKit.UITextField DataPartitionPhraseEntry { get; set; }

		[Outlet]
		UIKit.UITextField ImageCacheDurationEntry { get; set; }

		[Outlet]
		UIKit.UISwitch ResetToDefaultsSwitch { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DataPartitionPhraseEntry != null) {
				DataPartitionPhraseEntry.Dispose ();
				DataPartitionPhraseEntry = null;
			}

			if (BackendUrlEntry != null) {
				BackendUrlEntry.Dispose ();
				BackendUrlEntry = null;
			}

			if (ImageCacheDurationEntry != null) {
				ImageCacheDurationEntry.Dispose ();
				ImageCacheDurationEntry = null;
			}

			if (ClearImageCacheSwitch != null) {
				ClearImageCacheSwitch.Dispose ();
				ClearImageCacheSwitch = null;
			}

			if (ResetToDefaultsSwitch != null) {
				ResetToDefaultsSwitch.Dispose ();
				ResetToDefaultsSwitch = null;
			}
		}
	}
}

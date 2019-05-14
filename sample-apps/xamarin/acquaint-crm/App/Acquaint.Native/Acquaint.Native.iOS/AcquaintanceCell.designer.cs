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
	[Register ("AcquaintanceCell")]
	partial class AcquaintanceCell
	{
		[Outlet]
		UIKit.UILabel CompanyLabel { get; set; }

		[Outlet]
		UIKit.UILabel JobTitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView ProfilePhotoImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProfilePhotoImageView != null) {
				ProfilePhotoImageView.Dispose ();
				ProfilePhotoImageView = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (CompanyLabel != null) {
				CompanyLabel.Dispose ();
				CompanyLabel = null;
			}

			if (JobTitleLabel != null) {
				JobTitleLabel.Dispose ();
				JobTitleLabel = null;
			}
		}
	}
}

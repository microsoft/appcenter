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
	[Register ("AcquaintanceEditViewController")]
	partial class AcquaintanceEditViewController
	{
		[Outlet]
		UIKit.UITextField _CityField { get; set; }

		[Outlet]
		UIKit.UITextField _CompanyNameField { get; set; }

		[Outlet]
		UIKit.UIButton _DeleteButton { get; set; }

		[Outlet]
		UIKit.UITextField _EmailAddressField { get; set; }

		[Outlet]
		UIKit.UITextField _FirstNameField { get; set; }

		[Outlet]
		UIKit.UITextField _JobTitleField { get; set; }

		[Outlet]
		UIKit.UITextField _LastNameField { get; set; }

		[Outlet]
		UIKit.UITextField _PhoneNumberField { get; set; }

		[Outlet]
		UIKit.UITextField _StateField { get; set; }

		[Outlet]
		UIKit.UITextField _StreetField { get; set; }

		[Outlet]
		UIKit.UITextField _ZipField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_FirstNameField != null) {
				_FirstNameField.Dispose ();
				_FirstNameField = null;
			}

			if (_LastNameField != null) {
				_LastNameField.Dispose ();
				_LastNameField = null;
			}

			if (_CompanyNameField != null) {
				_CompanyNameField.Dispose ();
				_CompanyNameField = null;
			}

			if (_JobTitleField != null) {
				_JobTitleField.Dispose ();
				_JobTitleField = null;
			}

			if (_PhoneNumberField != null) {
				_PhoneNumberField.Dispose ();
				_PhoneNumberField = null;
			}

			if (_EmailAddressField != null) {
				_EmailAddressField.Dispose ();
				_EmailAddressField = null;
			}

			if (_StreetField != null) {
				_StreetField.Dispose ();
				_StreetField = null;
			}

			if (_CityField != null) {
				_CityField.Dispose ();
				_CityField = null;
			}

			if (_StateField != null) {
				_StateField.Dispose ();
				_StateField = null;
			}

			if (_ZipField != null) {
				_ZipField.Dispose ();
				_ZipField = null;
			}

			if (_DeleteButton != null) {
				_DeleteButton.Dispose ();
				_DeleteButton = null;
			}
		}
	}
}

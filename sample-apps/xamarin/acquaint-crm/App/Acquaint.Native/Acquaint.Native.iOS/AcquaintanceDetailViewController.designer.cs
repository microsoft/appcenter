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
	[Register ("AcquaintanceDetailViewController")]
	partial class AcquaintanceDetailViewController
	{
		[Outlet]
		UIKit.UIStackView _MainStackView { get; set; }

		[Outlet]
		UIKit.UILabel CityLabel { get; set; }

		[Outlet]
		UIKit.UILabel CompanyNameLabel { get; set; }

		[Outlet]
		UIKit.UIStackView DetailInfoHorizontalStackView { get; set; }

		[Outlet]
		UIKit.UIStackView DetailInfoStackView { get; set; }

		[Outlet]
		UIKit.UIStackView DetailInfoVerticalStackView { get; set; }

		[Outlet]
		UIKit.UIImageView DialNumberImageView { get; set; }

		[Outlet]
		UIKit.UILabel EmailLabel { get; set; }

		[Outlet]
		UIKit.UIImageView GetDirectionsImageView { get; set; }

		[Outlet]
		UIKit.UILabel JobTitleLabel { get; set; }

		[Outlet]
		UIKit.UIStackView MainStackView { get; set; }

		[Outlet]
		MapKit.MKMapView MapView { get; set; }

		[Outlet]
		UIKit.UIView MapViewContainer { get; set; }

		[Outlet]
		UIKit.UILabel PhoneLabel { get; set; }

		[Outlet]
		UIKit.UIImageView ProfilePhotoImageView { get; set; }

		[Outlet]
		UIKit.UIImageView SendEmailImageView { get; set; }

		[Outlet]
		UIKit.UIImageView SendMessageImageView { get; set; }

		[Outlet]
		UIKit.UILabel StateAndPostalLabel { get; set; }

		[Outlet]
		UIKit.UILabel StreetLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CityLabel != null) {
				CityLabel.Dispose ();
				CityLabel = null;
			}

			if (CompanyNameLabel != null) {
				CompanyNameLabel.Dispose ();
				CompanyNameLabel = null;
			}

			if (DetailInfoHorizontalStackView != null) {
				DetailInfoHorizontalStackView.Dispose ();
				DetailInfoHorizontalStackView = null;
			}

			if (DetailInfoVerticalStackView != null) {
				DetailInfoVerticalStackView.Dispose ();
				DetailInfoVerticalStackView = null;
			}

			if (EmailLabel != null) {
				EmailLabel.Dispose ();
				EmailLabel = null;
			}

			if (JobTitleLabel != null) {
				JobTitleLabel.Dispose ();
				JobTitleLabel = null;
			}

			if (MainStackView != null) {
				MainStackView.Dispose ();
				MainStackView = null;
			}

			if (MapViewContainer != null) {
				MapViewContainer.Dispose ();
				MapViewContainer = null;
			}

			if (PhoneLabel != null) {
				PhoneLabel.Dispose ();
				PhoneLabel = null;
			}

			if (ProfilePhotoImageView != null) {
				ProfilePhotoImageView.Dispose ();
				ProfilePhotoImageView = null;
			}

			if (StateAndPostalLabel != null) {
				StateAndPostalLabel.Dispose ();
				StateAndPostalLabel = null;
			}

			if (StreetLabel != null) {
				StreetLabel.Dispose ();
				StreetLabel = null;
			}

			if (_MainStackView != null) {
				_MainStackView.Dispose ();
				_MainStackView = null;
			}

			if (DetailInfoStackView != null) {
				DetailInfoStackView.Dispose ();
				DetailInfoStackView = null;
			}

			if (MapView != null) {
				MapView.Dispose ();
				MapView = null;
			}

			if (GetDirectionsImageView != null) {
				GetDirectionsImageView.Dispose ();
				GetDirectionsImageView = null;
			}

			if (SendMessageImageView != null) {
				SendMessageImageView.Dispose ();
				SendMessageImageView = null;
			}

			if (DialNumberImageView != null) {
				DialNumberImageView.Dispose ();
				DialNumberImageView = null;
			}

			if (SendEmailImageView != null) {
				SendEmailImageView.Dispose ();
				SendEmailImageView = null;
			}
		}
	}
}

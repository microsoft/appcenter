using System;
using System.Threading.Tasks;
using Acquaint.Data;
using Acquaint.Models;
using Acquaint.Util;
using FFImageLoading;
using FFImageLoading.Transformations;
using UIKit;

namespace Acquaint.Native.iOS
{
	/// <summary>
	/// Acquaintance cell. The layout for this Cell is defined almost entirely in Main.storyboard.
	/// </summary>
	public partial class AcquaintanceCell : UITableViewCell
	{
		// This constructor signature is required, for marshalling between the managed and native instances of this class.
		public AcquaintanceCell(IntPtr handle) : base(handle) { }

		/// <summary>
		/// Update the cell's child views' values and presentation.
		/// </summary>
		/// <param name="acquaintance">Acquaintance.</param>
		public void Update(Acquaintance acquaintance)
		{
			// set disclousure indicator accessory for the cell
			Accessory = UITableViewCellAccessory.DisclosureIndicator;

			NameLabel.Text = acquaintance.DisplayLastNameFirst;
			CompanyLabel.Text = acquaintance.Company;
			JobTitleLabel.Text = acquaintance.JobTitle;

			InvokeOnMainThread(async () => {
				// use FFImageLoading library to asynchronously:
				await ImageService
					.Instance
					.LoadUrl(acquaintance.SmallPhotoUrl, TimeSpan.FromHours(Settings.ImageCacheDurationHours))  // get the image from a URL
					.LoadingPlaceholder("placeholderProfileImage.png")                                          // specify a placeholder image
					.Transform(new CircleTransformation())                                                      // transform the image to a circle
					.Error(e => System.Diagnostics.Debug.WriteLine(e.Message))
					.IntoAsync(ProfilePhotoImageView);
			});
		}
	}
}

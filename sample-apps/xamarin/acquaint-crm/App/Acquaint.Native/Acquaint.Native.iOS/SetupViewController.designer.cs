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
	[Register ("SetupViewController")]
	partial class SetupViewController
	{
		[Outlet]
		UIKit.UIButton ContinueButton { get; set; }

		[Outlet]
		UIKit.UITextField DataPartitionPhraseEntry { get; set; }

		[Outlet]
		UIKit.UILabel InstructionsLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DataPartitionPhraseEntry != null) {
				DataPartitionPhraseEntry.Dispose ();
				DataPartitionPhraseEntry = null;
			}

			if (ContinueButton != null) {
				ContinueButton.Dispose ();
				ContinueButton = null;
			}

			if (InstructionsLabel != null) {
				InstructionsLabel.Dispose ();
				InstructionsLabel = null;
			}
		}
	}
}

using UIKit;

namespace Acquaint.Native.iOS
{
	public static class UINavigationControllerExtensions
	{
		public static void ApplyStyle(this UINavigationController navigationController)
		{
			// setting the NavigationBar.BarStyle to UIBarStyle.Black makes the status bar text and icons white.
			navigationController.NavigationBar.BarStyle = UIBarStyle.Black;

			// set some stlye properties foe the navigation bar
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(84, 119, 153);
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.White };
			UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White }, UIControlState.Normal);
		}
	}
}


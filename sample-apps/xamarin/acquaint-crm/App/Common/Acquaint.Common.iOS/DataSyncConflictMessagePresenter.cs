using Acquaint.Abstractions;
using UIKit;

namespace Acquaint.Common.iOS
{
	public class DataSyncConflictMessagePresenter : IDataSyncConflictMessagePresenter
	{
		bool _IsPresenting;

		public void PresentConflictMessage()
		{
			if (_IsPresenting)
				return;

			_IsPresenting = true;

			var app = UIApplication.SharedApplication;

			app.InvokeOnMainThread(() => {

				UIAlertController alert = UIAlertController.Create(
					"Data Sync Conflict", 
					"One or more of your local data updates is in conflict with data in the cloud, likely because someone else was editing the same data. Reset your local data and try again.", 
					UIAlertControllerStyle.Alert);

				// cancel button
				alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (UIAlertAction obj) => { _IsPresenting = false; }));

				app.KeyWindow.RootViewController.PresentViewController(alert, true, null);
			});
		}
	}
}


using Acquaint.Abstractions;
using Android.App;
using Plugin.CurrentActivity;

namespace Acquaint.Common.Droid
{
	public class DataSyncConflictMessagePresenter : IDataSyncConflictMessagePresenter
	{
		bool _IsPresenting;

		public void PresentConflictMessage()
		{
			if (_IsPresenting)
				return;

			_IsPresenting = true;

			var currentActivity = CrossCurrentActivity.Current.Activity;

 			currentActivity.RunOnUiThread(() => {
				//set alert for executing the task
				var alert = new AlertDialog.Builder(currentActivity);

				alert.SetTitle("Data Sync Conflict");

				alert.SetMessage("One or more of your local data updates is in conflict with data in the cloud, likely because someone else was editing the same data. Reset your local data and try again.");

				alert.SetPositiveButton("OK", (senderAlert, args) => {
					_IsPresenting = false;
				});

				//run the alert in UI thread to display in the screen
				alert.Show();
			});
		}
	}
}


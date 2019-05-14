using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

namespace Acquaint.Native.Droid
{
	/// <summary>
	/// Splash activity. Android doesn't support splash screens out of the box (like iOS), so we're making our own.
	/// </summary>
	[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/AcquaintTheme.Splash", Icon = "@mipmap/icon", NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override async void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// ensure that the system bar color gets drawn
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			// await a new task
			await Task.Factory.StartNew(async () => {

				// delay for 2 seconds on the splash screen
				await Task.Delay(2000);

				// start the AcquaintanceListActivity
				StartActivity(new Intent(Application.Context, typeof(AcquaintanceListActivity)));
			});
		}
	}
}


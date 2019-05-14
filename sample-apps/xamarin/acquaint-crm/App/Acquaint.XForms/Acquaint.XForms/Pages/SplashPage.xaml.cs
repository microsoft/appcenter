using Xamarin.Forms;
using System.Threading.Tasks;
using Acquaint.Util;

namespace Acquaint.XForms
{
    /// <summary>
    /// Splash Page that is used on Android only. iOS splash characteristics are NOT defined here, ub tn the iOS prject settings.
    /// </summary>
    public partial class SplashPage : ContentPage
    {
        bool _ShouldDelayForSplash = true;

        public SplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // call sign out so we start in a "normal"state for the demo
            
            if (_ShouldDelayForSplash)

                Microsoft.AppCenter.Auth.Auth.SignOut();
            // delay for a few seconds on the splash screen
            await Task.Delay(500);


            // if a data partition phrase has not yet been set
            //if (string.IsNullOrWhiteSpace(Settings.DataPartitionPhrase))
            //{
            //    // modally push a new SetupPage wrapped in a NavigationPage
            //    var navPage = new NavigationPage(new SetupPage())
            //    {
            //        BarBackgroundColor = Color.FromHex("547799")
            //    };

            //    navPage.BarTextColor = Color.White;

            //    await Navigation.PushModalAsync(navPage);

            //    _ShouldDelayForSplash = false;
            //}
            //else
            //{
                // create a new NavigationPage, with a new AcquaintanceListPage set as the Root
                var navPage = new NavigationPage(
                    new AcquaintanceListPage()
                    {
                        Title = "Acquaintances",
                        BindingContext = new AcquaintanceListViewModel()
                    });

                navPage.BarTextColor = Color.White;

                // set the MainPage of the app to the navPage
                Application.Current.MainPage = navPage;
            //}
        }
    }
}


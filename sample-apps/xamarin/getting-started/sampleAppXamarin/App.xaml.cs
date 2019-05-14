using System.Collections.Generic;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace sampleAppXamarin
{
    public partial class App : Application
    {
        public App()
        {
            AppCenter.Start("android=56a4c38c-9003-489d-b04b-c2c547fc23d5;" +
                   "ios=b21e0878-63f9-4fd0-bcbf-94d1760c7f91;",
                   typeof(Analytics), typeof(Crashes));
            MainPage = new ButtonPage();
        }
    }
}

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System;

namespace Acquaint.XForms
{
    public partial class AcquaintanceDetailPage : ContentPage
    {
        protected AcquaintanceDetailViewModel ViewModel => BindingContext as AcquaintanceDetailViewModel;

        public AcquaintanceDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Typically, is preferable to call into the viewmodel for OnAppearing() logic to be performed,
            // but we're not doing that in this case because we need to interact with the Xamarin.Forms.Map property on this Page.
            // In the future, the Map type and it's properties may get more binding support, so that the map setup can be omitted from code-behind.
            await SetupMap();
        }

        /// <summary>
        /// Sets up the map.
        /// </summary>
        /// <returns>A Task.</returns>
        async Task SetupMap()
        {
            if (ViewModel.HasAddress)
            {
                AcquaintanceMap.IsVisible = false;

                // set to a default position
                Position position;

                try
                {
                    position = await ViewModel.GetPosition();
                }
                catch (Exception ex)
                {
                    ViewModel.DisplayGeocodingError();

                    return;
                }

                // if lat and lon are both 0, then it's assumed that position acquisition failed
                if (position.Latitude == 0 && position.Longitude == 0)
                {
                    ViewModel.DisplayGeocodingError();

                    return;
                }

                // Xamarin.Forms.Maps (2.3.107) currently has a bug that causes map pins to throw ExecutionEngineExceptions on UWP.
                // Omitting pins on UWP for now.
                if (Device.OS != TargetPlatform.WinPhone && Device.OS != TargetPlatform.Windows)
                {
                    var pin = new Pin()
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = ViewModel.Acquaintance.DisplayName,
                        Address = ViewModel.Acquaintance.AddressString
                    };

                    AcquaintanceMap.Pins.Clear();

                    AcquaintanceMap.Pins.Add(pin);
                }

            AcquaintanceMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(10)));

                AcquaintanceMap.IsVisible = true;
            }
        }
    }
}


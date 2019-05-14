using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Acquaint.Abstractions;
using Acquaint.Util;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Acquaint.Models;
using Acquaint.Data;

namespace Acquaint.XForms.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        // an IoC Container
        IContainer _IoCContainer;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            RegisterDependencies();

            Settings.OnDataPartitionPhraseChanged += (sender, e) => {
                UpdateDataSourceIfNecessary();
            };

            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                //this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Xamarin.Forms.Forms.Init(e);

                Xamarin.FormsMaps.Init(Settings.BingMapsKey);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            SetTitleBarStatusBarColor();
        }

        void SetTitleBarStatusBarColor()
        {
            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = Color.FromArgb(255, 23, 40, 57);
                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.BackgroundColor = Color.FromArgb(255, 23, 40, 57);
                    titleBar.ForegroundColor = Colors.White;
                }
            }

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {

                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = Color.FromArgb(255, 23, 40, 57);
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }



        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        /// <summary>
		/// Registers dependencies with an IoC container.
		/// </summary>
		/// <remarks>
		/// Since some of our libraries are shared between the Forms and Native versions 
		/// of this app, we're using an IoC/DI framework to provide access across implementations.
		/// </remarks>
		void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new EnvironmentService()).As<IEnvironmentService>();

            builder.RegisterInstance(new HttpClientHandlerFactory()).As<IHttpClientHandlerFactory>();

            builder.RegisterInstance(new DatastoreFolderPathProvider()).As<IDatastoreFolderPathProvider>();

            // Set the data source dependent on whether or not the data parition phrase is "UseLocalDataSource".
            // The local data source is mainly for use in TestCloud test runs, but the app can be used in local-only data mode if desired.
            if (Settings.IsUsingLocalDataSource)
                builder.RegisterInstance(_LazyFilesystemOnlyAcquaintanceDataSource.Value).As<IDataSource<Acquaintance>>();
            else
                builder.RegisterInstance(_LazyAzureAcquaintanceSource.Value).As<IDataSource<Acquaintance>>();

            _IoCContainer = builder.Build();

            var csl = new AutofacServiceLocator(_IoCContainer);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        /// <summary>
		/// Updates the data source if necessary.
		/// </summary>
		void UpdateDataSourceIfNecessary()
        {
            var dataSource = ServiceLocator.Current.GetInstance<IDataSource<Acquaintance>>();

			// Set the data source dependent on whether or not the data parition phrase is "UseLocalDataSource".
			// The local data source is mainly for use in TestCloud test runs, but the app can be used in local-only data mode if desired.

			// if the settings dictate that a local data source should be used, then register the local data provider and update the IoC container
			if (Settings.IsUsingLocalDataSource && !(dataSource is FilesystemOnlyAcquaintanceDataSource))
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(_LazyFilesystemOnlyAcquaintanceDataSource.Value).As<IDataSource<Acquaintance>>();
                builder.Update(_IoCContainer);
                return;
            }

            // if the settings dictate that a local data souce should not be used, then register the remote data source and update the IoC container
            if (!Settings.IsUsingLocalDataSource && !(dataSource is AzureAcquaintanceSource))
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(_LazyAzureAcquaintanceSource.Value).As<IDataSource<Acquaintance>>();
                builder.Update(_IoCContainer);
            }
        }

        // we need lazy loaded instances of these two types hanging around because if the registration on IoC container changes at runtime, we want the same instances
        static Lazy<FilesystemOnlyAcquaintanceDataSource> _LazyFilesystemOnlyAcquaintanceDataSource = new Lazy<FilesystemOnlyAcquaintanceDataSource>(() => new FilesystemOnlyAcquaintanceDataSource());
        static Lazy<AzureAcquaintanceSource> _LazyAzureAcquaintanceSource = new Lazy<AzureAcquaintanceSource>(() => new AzureAcquaintanceSource());
    }
}

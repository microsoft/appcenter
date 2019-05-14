using System;
using Acquaint.Abstractions;
using Acquaint.Common.Droid;
using Acquaint.Data;
using Acquaint.Models;
using Acquaint.Util;
using Android.App;
using Android.OS;
using Android.Runtime;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using HockeyApp.Android;
using Microsoft.Practices.ServiceLocation;
using Plugin.CurrentActivity;

namespace Acquaint.Native.Droid
{
	//You can specify additional application information in this attribute
	[Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
		// an IoC Container
		IContainer _IoCContainer;

        public MainApplication(IntPtr handle, JniHandleOwnership transer) :base(handle, transer) { }

        public override void OnCreate()
        {
			RegisterDependencies();

			Settings.OnDataPartitionPhraseChanged += (sender, e) => {
				UpdateDataSourceIfNecessary();
			};

			// Azure Mobile Services initilization
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            base.OnCreate();

			CrashManager.Register(this, Settings.HockeyAppId);

            RegisterActivityLifecycleCallbacks(this);
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

        // We need lazy-loaded instances of these two types hanging around because if the registration on IoC container changes at runtime, we want the same instances.
		// Using a static Lazy<T> is a nice way to create the instances only if and when we need them.
        static Lazy<FilesystemOnlyAcquaintanceDataSource> _LazyFilesystemOnlyAcquaintanceDataSource = new Lazy<FilesystemOnlyAcquaintanceDataSource>(() => new FilesystemOnlyAcquaintanceDataSource());
        static Lazy<AzureAcquaintanceSource> _LazyAzureAcquaintanceSource = new Lazy<AzureAcquaintanceSource>(() => new AzureAcquaintanceSource());

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}
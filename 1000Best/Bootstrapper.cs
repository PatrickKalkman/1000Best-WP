using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Mom.WP8.Common;
using _1000Best.Common;
using _1000Best.Logging;
using _1000Best.Model;
using _1000Best.Network;
using _1000Best.ViewModels;
using DebugLogger = _1000Best.Common.DebugLogger;

namespace _1000Best
{
    public class Bootstrapper : PhoneBootstrapper
    {
        private PhoneContainer container;
        
        private LocalyticsSession appSession;

        public Bootstrapper()
        {
            LogManager.GetLog = type => new DebugLogger(type);
        }

        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
            return new TransitionFrame();
        }

        protected override void Configure()
        {
            container = new PhoneContainer(RootFrame);

            container.RegisterPhoneServices();
            container.PerRequest<MainPageViewModel>();
            container.PerRequest<PrivacyViewModel>();
            container.PerRequest<BackgroundImageBrush>();
            container.PerRequest<BackgroundImageRotator>();
            container.PerRequest<YearViewModel>();
            container.RegisterInstance(typeof (ILogging), null, new Logging.DebugLogger());
            container.RegisterSingleton(typeof(SongManager), null, typeof(SongManager));
            container.RegisterSingleton(typeof(BestSettings), null, typeof(BestSettings));
            container.PerRequest<ArtistViewModel>();
            container.PerRequest<ArtistImageUrlRetriever>();
            container.PerRequest<BestHttpClient>();
        }

        private void OpenLocalytics()
        {
            appSession = new LocalyticsSession("ygygygygygyg-8dfe7e46-b620-11e2-0d1c-76776");
            appSession.open();
            appSession.upload();            
        }

        protected override void OnActivate(object sender, Microsoft.Phone.Shell.ActivatedEventArgs e)
        {
            OpenLocalytics();
            base.OnActivate(sender, e);
        }

        protected override void OnLaunch(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e)
        {
            OpenLocalytics();
            UpdateReviewCounter();
            base.OnLaunch(sender, e);
        }

        private static void UpdateReviewCounter()
        {
            IsolatedStorageSettings.ApplicationSettings["askforreview"] = false;

            int started = 0;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("started"))
            {
                started = (int) IsolatedStorageSettings.ApplicationSettings["started"];
            }
            started++;
            IsolatedStorageSettings.ApplicationSettings["started"] = started;
            if (started == 3)
            {
                IsolatedStorageSettings.ApplicationSettings["askforreview"] = true;
            }
        }

        protected override void OnDeactivate(object sender, Microsoft.Phone.Shell.DeactivatedEventArgs e)
        {
            if (appSession != null)
            {
                appSession.close();
            }
            base.OnDeactivate(sender, e);
        }

        protected override void OnClose(object sender, Microsoft.Phone.Shell.ClosingEventArgs e)
        {
            appSession.close();
            base.OnClose(sender, e);
        }

        protected override void OnUnhandledException(object sender, System.Windows.ApplicationUnhandledExceptionEventArgs e)
        {
            Dictionary<String, String> attributes = new Dictionary<string, string>();
            attributes.Add("exception", e.ExceptionObject.Message);
            appSession.tagEvent("App crash", attributes);
            base.OnUnhandledException(sender, e);
        }
        
        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
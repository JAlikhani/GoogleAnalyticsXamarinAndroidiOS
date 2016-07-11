using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Analytics;

namespace GoogleAnalyticsXamarin.Droid.Services
{
    class GoogleAnalyticsService
    {
        public string TrackingId = "UA-80589606-1";

        private static GoogleAnalytics GAInstance;
        private static Tracker GATracker;

        public struct GAEventCategory
        {
            public static String MainActivity { get { return "Main Page"; } set { } }
        };

        #region Instantiation ...
        private static GoogleAnalyticsService thisRef;
        private GoogleAnalyticsService()
        {
            // no code req'd
        }

        public static GoogleAnalyticsService GetGASInstance()
        {
            if (thisRef == null)
                // it's ok, we can call this constructor
                thisRef = new GoogleAnalyticsService();
            return thisRef;
        }
        #endregion
        public void Initialize(Context context)
        {
            GAInstance = GoogleAnalytics.GetInstance(context.ApplicationContext);
            GAInstance.SetLocalDispatchPeriod(10);

            GATracker = GAInstance.NewTracker(TrackingId);
            GATracker.EnableExceptionReporting(true);
            GATracker.EnableAdvertisingIdCollection(false);
            GATracker.EnableAutoActivityTracking(true);
        }

        public void TrackAppPage(String pageNameToTrack)
        {
            GATracker.SetScreenName(pageNameToTrack);
            GATracker.Send(new HitBuilders.ScreenViewBuilder().Build());
        }

        public void TrackAppEvent(String eventCategory, String eventToTrack)
        {
            HitBuilders.EventBuilder builder = new HitBuilders.EventBuilder();
            builder.SetCategory(eventCategory);
            builder.SetAction(eventToTrack);
            builder.SetLabel("AppEvent");

            GATracker.Send(builder.Build());
        }
        public void TrackAppException(String exceptionMessageToTrack, Boolean isFatalException)
        {
            HitBuilders.ExceptionBuilder builder = new HitBuilders.ExceptionBuilder();
            builder.SetDescription(exceptionMessageToTrack);
            builder.SetFatal(isFatalException);

            GATracker.Send(builder.Build());
        }
    }
}
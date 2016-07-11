using System;

#if __IOS__
using Google.Analytics;
#endif

#if __ANDROID__
using Android.Gms.Analytics;
#endif

namespace GoogleAnalyticsXamarin.Shared
{
	public class MySharedMainClass
	{
#if __IOS__
        String os = "iOS";
        public ITracker Tracker;
#endif
#if __ANDROID__
        String os = "Android";
        private Tracker GATracker;
#endif
        int count = 1;

        public string Click()
        {
            return string.Format("{0} clicks! (Shared-{1})", count++, os);
        }
    }
}


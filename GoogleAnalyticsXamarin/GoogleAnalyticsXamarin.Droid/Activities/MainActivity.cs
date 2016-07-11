using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using GoogleAnalyticsXamarin.Droid.Services;
using GoogleAnalyticsXamarin.Shared;

namespace GoogleAnalyticsXamarin.Droid.Activities
{
	[Activity (Label = "GoogleAnalyticsXamarin.Droid", MainLauncher = true, Icon = "@drawable/Jcon")]
	public class MainActivity : Activity
	{
        MyPCLMainClass myPCLMainClass;
        Shared.MySharedMainClass mySharedMainClass;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            myPCLMainClass = new MyPCLMainClass();
            mySharedMainClass = new MySharedMainClass();
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
                button.Text = myPCLMainClass.Click() + " " + mySharedMainClass.Click();

                // Track an event
                GoogleAnalyticsService.GetGASInstance().TrackAppEvent(GoogleAnalyticsService.GAEventCategory.MainActivity, "Button Clicked");
                try
                {
                    throw new ArgumentOutOfRangeException();
                }
                catch (Exception ex)
                {
                    // Track an Exception
                    GoogleAnalyticsService.GetGASInstance().TrackAppException("Crash Happend in Android", true);
                    Console.WriteLine("Event Sent, Check Google's GA Event Console");
                }
            };

            GoogleAnalyticsService.GetGASInstance().Initialize(this);

            // Track a page 
            GoogleAnalyticsService.GetGASInstance().TrackAppPage("Main Activity - Android");
        }
	}
}



using GoogleAnalyticsXamarin.Shared;
using System;
using UIKit;
using Google.Analytics;

namespace GoogleAnalyticsXamarin.iOS
{
	public partial class ViewController : UIViewController
	{
        MyPCLMainClass myPCLMainClass;
        MySharedMainClass mySharedMainClass;

        public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            myPCLMainClass = new MyPCLMainClass();
            mySharedMainClass = new MySharedMainClass();
            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate {
				var title = myPCLMainClass.Click() + " " + mySharedMainClass.Click();
                Button.SetTitle (title, UIControlState.Normal);

                var gaEvent = DictionaryBuilder.CreateEvent("UI", "Button Clicked", "Count Up", null).Build();
                Gai.SharedInstance.DefaultTracker.Send(gaEvent);
                Gai.SharedInstance.Dispatch();

                // Create a Crash
                try
                {
                    throw new ArgumentOutOfRangeException();
                }
                catch (Exception ex)
                {
                    // Track an Exception
                    Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateException("Crash Happend in iOS", true).Build());
                    Console.WriteLine("Event Sent, Check Google's GA Event Console");
                }
                
            };
		}

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // This screen name value will remain set on the tracker and sent with
            // hits until it is set to a new value or to null.
            Gai.SharedInstance.DefaultTracker.Set(GaiConstants.ScreenName, "Main View Controller - iOS");

            Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateScreenView().Build());
        }

        public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


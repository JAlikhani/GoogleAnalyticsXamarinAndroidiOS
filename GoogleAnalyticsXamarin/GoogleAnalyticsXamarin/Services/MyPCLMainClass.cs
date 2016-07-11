using System;

namespace GoogleAnalyticsXamarin
{
    public class MyPCLMainClass
    {
        int count = 1;

        public string Click()
        {
            return string.Format("{0} clicks! (PCL)", count++);
        }
    }
}


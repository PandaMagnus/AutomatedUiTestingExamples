using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

namespace AttachToChrome.Tests.AttachToChrome
{
    public class Application
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        public static string AttachToChromeApplicationLocation
        {
            get
            {
                string appLocation = Path.GetFullPath(@"..\..\..");
                return Path.Combine(appLocation, @"AttachToChrome\bin\Debug");
            }
        }

        public static WindowsDriver<WindowsElement> Session { get; set; }

        public static void LaunchApplication()
        {
            CloseApplication();
            if (Session == null)
            {
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", AttachToChromeApplicationLocation + "\\AttachToChrome.exe");
                Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            }
        }

        public static void CloseApplication()
        {
            if (Session != null)
            {
                Session.Quit();
                Session = null;
            }
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
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

        public static void AttachToApplication()
        {
            CloseApplication();
            // Get the root desktop
            DesiredCapabilities rootCapabilities = new DesiredCapabilities();
            rootCapabilities.SetCapability("app", "Root");
            // Create a session for the desktop
            WindowsDriver<WindowsElement> desktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), rootCapabilities);

            // Set up a loop so it doesn't fail if it tries to find the window too soon. Note: if you have multiple cases where you need to wait for a window to be present, it's best to abstract this logic out to a helper function.
            WebDriverWait wait = new WebDriverWait(desktopSession, TimeSpan.FromSeconds(10));
            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
            WindowsElement appWindow = null;
            appWindow = wait.Until(w =>
            {
                return (WindowsElement)w.FindElement(By.Name("MainWindow"));
            });

            // After getting the window, get it's handle
            string appHandle = appWindow.GetAttribute("NativeWindowHandle");
            // Convert to Hex
            appHandle = (int.Parse(appHandle)).ToString("x");
            // Create session by attaching to the top level window
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("appTopLevelWindow", appHandle);
            Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
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

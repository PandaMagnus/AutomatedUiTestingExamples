using OpenQA.Selenium.Appium.Windows;

namespace AttachToChrome.Tests.AttachToChrome
{
    public class Window
    {
        public WindowsElement LaunchBrowserButton => Application.Session.FindElementByAccessibilityId("launchBrowserButton");
    }
}

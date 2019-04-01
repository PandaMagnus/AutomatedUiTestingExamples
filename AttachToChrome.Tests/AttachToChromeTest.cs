using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttachToChrome.Tests.AttachToChrome;
using System.Diagnostics;
using AttachToChrome.Tests.IntelliTect;
using System.Threading.Tasks;

namespace AttachToChrome.Tests
{
    [TestClass]
    public class AttachToChromeTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestInitialize()]
        public void MyTestInitialize()
        {
            Process[] wap = Process.GetProcessesByName("WinAppDriver");
            if (wap.Length < 1)
            {
                Process.Start(@"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe");
            }
            Process[] btb = Process.GetProcessesByName("AttachToChrome");
            if (btb.Length > 0)
            {
                foreach (var p in btb)
                {
                    p.Kill();
                }
            }

            Application.LaunchApplication();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            Application.CloseApplication();
            _Page.Driver.Quit();
            // Because we attached, the browser itself won't close.
        }


        [TestMethod]
        public void LaunchChromeAndAttach()
        {
            // Do this properly before blog
            Task.Delay(5000).Wait();
            Application.Session.SwitchTo().Window(Application.Session.CurrentWindowHandle);
            _Window.LaunchBrowserButton.Click();
            _Page.AttachToChrome();
            Assert.AreEqual("https://intellitect.com/blog/", _Page.Driver.Url);
            Assert.IsTrue(_Page.BlogList.Displayed);
            Assert.IsTrue(_Page.BlogHeadings.Count > 0);
        }

        private Window _Window => new Window();
        private Page _Page { get; } = new Page();
    }
}

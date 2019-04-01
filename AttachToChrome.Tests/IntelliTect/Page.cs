using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttachToChrome.Tests.IntelliTect
{
    public class Page
    {
        public IWebDriver Driver { get; private set; }
        public IWebElement BlogList => Driver.FindElement(By.CssSelector("div[class^='template-blog']"));
        public IReadOnlyCollection<IWebElement> BlogHeadings => BlogList.FindElements(By.TagName("h3"));

        public void AttachToChrome()
        {
            // Do this the proper way before writing blog
            Task.Delay(5000).Wait();
            ChromeOptions options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:9222";
            Driver = new ChromeDriver(options);
        }

        

        //private IWebElement WaitForElementToExist()
        //{

        //}
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Polly;
using System;
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
            ChromeOptions options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:9222";

            // Polly probably isn't needed in a single scenario like this, but can be useful in a broader automation project
            var policy = Policy
                .Handle<InvalidOperationException>()
                .WaitAndRetry(10, t => TimeSpan.FromSeconds(1));
            policy.Execute(() => 
                {
                    Driver = new ChromeDriver(options);
                });
        }
    }
}

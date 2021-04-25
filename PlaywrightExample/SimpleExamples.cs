using PlaywrightSharp;
using System.Threading.Tasks;
using Xunit;

namespace PlaywrightExample
{
    public class SimpleExamples
    {
        [Fact]
        public async Task NavigateToPage()
        {
            using var pw = await Playwright.CreateAsync();
            await using var browser = await pw.Chromium.LaunchAsync(headless: false);
            var page = await browser.NewPageAsync();
            await page.GoToAsync("http://the-internet.herokuapp.com/");
            Assert.True(await page.IsVisibleAsync("css=h1[class='heading']"));
        }
    }
}

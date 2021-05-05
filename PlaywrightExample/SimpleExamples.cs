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

        [Fact]
        public async Task ClickScenario()
        {
            using var pw = await Playwright.CreateAsync();
            await using var browser = await pw.Chromium.LaunchAsync(headless: false);
            var page = await browser.NewPageAsync();
            await page.GoToAsync("https://the-internet.herokuapp.com/tinymce");

            await page.ClickAsync("//span[text() = 'Format']");
            await page.ClickAsync("//div[text() = 'Formats']");
            await page.ClickAsync("//div[text() = 'Headings']");
            await page.ClickAsync("//h1[text() = 'Heading 1']");

            Assert.Equal("Heading 1", await page.GetTextContentAsync("span[class='tox-tbtn__select-label']"));
        }
    }
}

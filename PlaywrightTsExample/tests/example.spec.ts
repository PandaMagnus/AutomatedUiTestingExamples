import { test, expect, Page } from '@playwright/test';

test('basic test', async ({page}) => {
    await page.goto('https://the-internet.herokuapp.com/add_remove_elements/');
    await page.click("css=button[onclick='addElement()']");
    const delLocator = page.locator("css=button.added-manually");
    await expect(delLocator).toBeVisible();
});
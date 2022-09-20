using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Shared;

namespace DataAccessLayer
{


    public static class WebScraperDAL
    {
        public static List<string> ScrapeInstagramWithDefaultAccount(bool headless, string profile)
        {

            IWebDriver driver;
            ChromeOptions options = new ChromeOptions();

            // Set launch args similar to puppeteer's for best performance
            options.AddArgument("--disable-background-timer-throttling");
            options.AddArgument("--disable-backgrounding-occluded-windows");
            options.AddArgument("--disable-breakpad");
            options.AddArgument("--disable-component-extensions-with-background-pages");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-features=TranslateUI,BlinkGenPropertyTrees");
            options.AddArgument("--disable-ipc-flooding-protection");
            options.AddArgument("--disable-renderer-backgrounding");
            options.AddArgument("--enable-features=NetworkService,NetworkServiceInProcess");
            options.AddArgument("--force-color-profile=srgb");
            options.AddArgument("--hide-scrollbars");
            options.AddArgument("--metrics-recording-only");
            options.AddArgument("--mute-audio");
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--private");
            options.AddArgument("--deny-permission-prompts");

            // Note we set our token here, with `true` as a third arg
            options.AddAdditionalOption("browserless:token", "6d199b73-289d-40dd-aff9-a36e25a5623a");

            driver = new RemoteWebDriver(
              new Uri("https://chrome.browserless.io/webdriver"), options.ToCapabilities()
            );

            //opens the website
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            driver.Navigate().GoToUrl("https://www.instagram.com/");


            //waits and targets the username and password inputs
            IWebElement username = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("username")));
            IWebElement password = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("password")));


            //writes the account's username and password and clicks to login
            username.SendKeys(DotEnv.DEFAULT_USERNAME);
            password.SendKeys(DotEnv.DEFAULT_PASSWORD);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[type='submit']"))).Click();


            //searches the user profile
            wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("_acuj")));
            driver.Navigate().GoToUrl("https://www.instagram.com/" + profile);



            //scrolls down to scrape more images
            //maybe the index could be a parameter, so the user could define how much they want to scroll

            wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("_aabd")));

            //target all images on the page
            //for (int i = 1; i < 6; i++)
            //{
            //    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div/div[3]/article/div[1]/div/div[{i}]")));
            //    driver.ExecuteJavaScript("window.scrollTo(0, 4000);");

            //}

            //O DOM ATUALIZA E TAMO PERDENDENDO AS IMAGENS DE CIMA QUANDO DESCEMOS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            var imgs = driver.FindElements(By.TagName("img"));
            List<string> sources = new();

            foreach (var img in imgs)
            {
                sources.Add(img.GetAttribute("src").ToString());
                //if (sources.Count > 21)
                //{
                //    return sources;
                //}
            }

            //driver.Close();
            driver.Quit();

            return sources.SkipLast(1).ToList();
        }

    }
}
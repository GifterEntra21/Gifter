using System.Collections.ObjectModel;
using DataAccessLayer.Interfaces;
using Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Shared;

namespace DataAccessLayer.Impl
{
    public class WebScrapperDAL : IWebScrapperDAL
    {
        public  async Task<List<string>> ScrapeInstagramWithDefaultAccount(bool headless, string profile)
        {

            FirefoxOptions options = new();

            //makes the browser invisible
            if (headless)
            {
                options.AddArgument("--headless");
            }
            options.AddArgument("--private");
            options.BrowserExecutableLocation = "../Shared/Assets/firefox/firefox.exe";

            FirefoxDriver driver = new FirefoxDriver("../Shared/Assets", options);

            //opens the website
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            driver.Navigate().GoToUrl("https://www.instagram.com/" );


            //waits and targets the username and password inputs
            IWebElement username = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("username")));
            IWebElement password = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("password")));


            //writes the account's username and password and clicks to login
            SocialMediaAccount sca = await CosmosDb.GetInstagramAccount();
            username.SendKeys(sca.Email);
            password.SendKeys(sca.Password);

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
            //    driver.ExecuteScript("window.scrollTo(0, 4000);");

            //}

           

            var imgs = driver.FindElements(By.TagName("img"));
            List<string> sources = new();

            foreach (var img in imgs)
            {
                sources.Add(img.GetAttribute("src").ToString());
            }

            driver.Quit();

            return sources.SkipLast(1).ToList();
        }

    }
}
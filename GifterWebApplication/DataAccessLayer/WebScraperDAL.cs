
using Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Shared.Resposes;


namespace DataAccessLayer
{


    public static class WebScraperDAL
    {

        public static DataResponse<string> ScrapeInstagramWithDefaultAccount(bool headless, string profile)
        {
            

            try
            {
                
                FirefoxOptions options = new();

                //makes the browser invisible
                if (headless)
                {
                    options.AddArgument("--headless");
                }
                options.AddArgument("--incognito");
                options.BrowserExecutableLocation = "../Shared/Assets/firefox/firefox.exe";

                FirefoxDriver driver = new FirefoxDriver("../Shared/Assets", options);

                //opens the website
                driver.Navigate().GoToUrl("https://www.instagram.com/");

                //waits and targets the username and password inputs
                WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                IWebElement username = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[1]/div/label/input")));
                IWebElement password = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[2]/div/label/input")));


                //writes the account's username and password and clicks to login
                //username.SendKeys(DotEnv.DEFAULT_USERNAME);
                //password.SendKeys(DotEnv.DEFAULT_PASSWORD);
                //username.SendKeys(DotEnv.DEFAULT_USERNAME);
                //password.SendKeys(DotEnv.DEFAULT_PASSWORD);

                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[3]/button"))).Click();

                //handle pop ups
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/div/div/div/div/button"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[3]/button[2]"))).Click();

                //searches the user profile

                IWebElement search = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/div[1]/section/nav/div[2]/div/div/div[2]/input")));
                search.SendKeys(profile);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/div[1]/section/nav/div[2]/div/div/div[2]/div[3]/div/div[2]/div/div[1]/a/div"))).Click();

                //scrolls down to scrape more images
                //maybe the index could be a parameter, so the user could define how much they want to scroll

                //for (int i = 1; i < 16; i = i + 4)
                //{
                //    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/div[1]/section/main/div/div[3]/article/div[1]/div/div[" + i + "]")));
                //    driver.ExecuteScript("window.scrollTo(0, 4000);");
                //}
                //target all images on the page

                //O DOM ATUALIZA E TAMO PERDENDENDO AS IMAGENS DE CIMA QUANDO DESCEMOS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/div[1]/section/main/div/div[3]")));
                var imgs = driver.FindElements(By.TagName("img"));
                List<string> sources = new();

                foreach (var img in imgs)
                {
                    sources.Add(img.GetAttribute("src").ToString());
                }


                return new DataResponse<string>
                {
                    HasSucces = true,
                    Message = "Imagens selecionadas com sucesso",
                    Item = sources.SkipLast(1).ToList()
                };
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("DOM"))
                {
                    return new DataResponse<string>
                    {
                        HasSucces = false,
                        Message = "Não foi possível localizar as imagens",
                        Item = null
                    };                 
                }
                return new DataResponse<string>
                {
                    HasSucces = false,
                    Message= "Erro interno",
                    Item = null
                };
            }
            
        }

    }
}
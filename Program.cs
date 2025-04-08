using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        // Launch Chrome (no special options needed for online URL)
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize();

        try
        {
            // Load the online page
            string url = "https://app.cloudqa.io/home/AutomationPracticeForm";
            driver.Navigate().GoToUrl(url);

            // Wait for page to load dynamically
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            // Find and fill First Name field
            IWebElement firstName = FindElementRobustly(driver, wait, By.Id("Fname"), By.XPath("//input[@placeholder='Name']"));
            firstName.SendKeys("Deepanshu");

            // Fill Email field
            IWebElement emailField = FindElementRobustly(driver, wait, By.Id("Email"), By.XPath("//input[@placeholder='Email']"));
            emailField.SendKeys("deepanshu@example.com");

            // Fill Mobile Number field
            IWebElement mobileField = FindElementRobustly(driver, wait, By.Id("Mobile"), By.XPath("//input[@placeholder='Mobile Number']"));
            mobileField.SendKeys("9876543210");

            // Verify the fields
            bool nameCheck = firstName.GetAttribute("value") == "Deepanshu";
            bool emailCheck = emailField.GetAttribute("value") == "deepanshu@example.com";
            bool mobileCheck = mobileField.GetAttribute("value") == "9876543210";

            if (nameCheck && emailCheck && mobileCheck)
            {
                Console.WriteLine("Test Passed: All fields filled and verified.");
            }
            else
            {
                Console.WriteLine("Test Failed: One or more fields not filled correctly.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception occurred: " + e.Message);
        }

        // Keep browser open for inspection
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        driver.Quit();
    }

    // Helper method to find elements robustly with fallbacks
    static IWebElement FindElementRobustly(IWebDriver driver, WebDriverWait wait, By primaryLocator, By fallbackLocator)
    {
        try
        {
            return wait.Until(d => d.FindElement(primaryLocator));
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine($"Primary locator {primaryLocator} failed, trying fallback...");
            return wait.Until(d => d.FindElement(fallbackLocator));
        }
    }
}

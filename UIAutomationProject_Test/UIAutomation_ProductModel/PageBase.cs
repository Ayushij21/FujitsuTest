using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace UIAutomation_ProductModel
{
    public class PageBase
    {
        public IWebDriver driver;

        public PageBase(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool WaitForElementToVisible(IWebElement ele)
        {
            bool isDisplayed = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver web) =>
            {
                IWebElement element = ele;
                if (element.Displayed)
                {
                    isDisplayed = true;
                }
                return isDisplayed;
            });
            wait.Until(waitForElement);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return isDisplayed;
        }
        /// <summary>
        /// Method use to switch to tab as per provided index
        /// </summary>
        /// <param name="indexOfTab">Index of tab</param>
        public void SwitchToTabByIndex(int indexOfTab)
        {
            //Get number of current Tabs 
            int noOfTabs = driver.WindowHandles.Count;

            //If provided index is greater than current window handle then switch to latest open tab 
            if (indexOfTab > noOfTabs)
            {
                //Switch to latest open tab 
                driver.SwitchTo().Window(driver.WindowHandles.Last());
            }
            else
            {
                //Switch to provided index of open tab 
                driver.SwitchTo().Window(driver.WindowHandles[indexOfTab - 1]);
            }
        } 
            

        public void Refresh()
        {
            driver.Navigate().Refresh();
        }
        }

    
    }

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomation_ProductModel
{
    public class ItemPage :PageBase
    {

        public ItemPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement DropDown => driver.FindElement(By.Id("quantity"));
        private IWebElement PriceElement => driver.FindElement(By.XPath("//div[@id='corePriceDisplay_desktop_feature_div']//span[@class='a-price-whole']"));

        private IWebElement AddToCartButton => driver.FindElement(By.Id("add-to-cart-button"));

        public void SelectItemSize(string value)
        {
            SelectElement select = new SelectElement(DropDown);
            select.SelectByValue(value);
        }

        public void AddToCart()
        {

            AddToCartButton.Click();
        }

        public int Getprice()
        {
           int price = int.Parse(PriceElement.Text.Replace(",", ""));
                        return price;
        }
    }
}

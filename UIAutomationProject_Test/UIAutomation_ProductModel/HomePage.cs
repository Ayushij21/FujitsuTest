using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomation_ProductModel
{
   
    public class HomePage : PageBase
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement elementForMobile => driver.FindElement(By.XPath("//a[@data-csa-c-content-id='nav_cs_mobiles']"));
        private  IWebElement SamsungFilter => driver.FindElement(By.XPath("//*[text()='Brands']/parent::div/following-sibling::ul//a//span[text()='Samsung']"));

        IList<IWebElement> ItemList => driver.FindElements(By.XPath("//h2//span/parent::a/span"));
        private IWebElement CartElement => driver.FindElement(By.Id("nav-cart"));
        private IWebElement CartElementCount => driver.FindElement(By.Id("nav-cart-count"));

       
        private IWebElement GetSubTotal => driver.FindElement(By.XPath("//div[@data-name='Subtotals']//span[@id='sc-subtotal-amount-buybox']/span"));


        public void ClickLinkFromBar(QuickBarLinks linkToClick)
        {
            IWebElement ele = null; 
            switch (linkToClick)
            {
                case QuickBarLinks.BestSeller:
                    break;
                case QuickBarLinks.Mobiles:
                    ele = elementForMobile;
                    break;
            }

            ele.Click();
        }

        public void SelectBrand(string brandName)
        {
            IWebElement brandFilter=  driver.FindElement(By.XPath($"//*[text()='Brands']/parent::div/following-sibling::ul//a//span[text()='{brandName}']"));
            brandFilter.Click();
        }

        public void SelectItem(string value)
        {
            IWebElement itemToSelect = driver.FindElement(By.XPath($"//h2//span[text()='{value}']/parent::a"));
            itemToSelect.Click();
        }

        public List<string> GetListOfItems()
        {
            List<string> actualTypeFilters = new List<string>();
            foreach (var item in ItemList)
            {
                string filter = item.Text.Trim();
                actualTypeFilters.Add(filter);
            }

            return actualTypeFilters;

        }

        public void CLickOnCart()
        {
            CartElement.Click();
        }

        public int GetCartCount()
        {
            int price = int.Parse(CartElementCount.Text);
            return price;
        }

        public int GetTotalOfCart()
        {
            string subTotalText = GetSubTotal.Text.Trim().Replace(",", "");
         
            string subTotalNum = subTotalText.Remove(subTotalText.IndexOf("."));
          
            int priceInTotal = int.Parse(subTotalNum);
            return priceInTotal;
        }
        public enum QuickBarLinks
        {
            BestSeller,
            Mobiles
        }

    }
}

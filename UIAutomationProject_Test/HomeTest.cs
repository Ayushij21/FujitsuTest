using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UIAutomation_ProductModel;


namespace UIAutomationProject_Test
{
    [TestClass]
    public class HomeTest : TestBase
    {
        private string appUrl { get; set; } = "https://www.amazon.in";

        [TestInitialize]
        public void TestInit()
        {
            LaunchApplication(appUrl);
            test = extent.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void Cleanup()
        {
            QuitBrowser();
        }

        [Description("HAPPY PATH, PURCHASE 2 ITEMS")]
        [TestMethod]
        public void VerifyFinalCartValue_Pass()
        {

            //Select Mobiles from quick link bar  and selected samsung brand
            HomePage homePage = new HomePage(driver);
            homePage.ClickLinkFromBar(HomePage.QuickBarLinks.Mobiles);
            homePage.SelectBrand("Samsung");

            //Get all list of items under samsung filter and select the first item
            List<string> listOfItems = homePage.GetListOfItems();
            homePage.SelectItem(listOfItems[1]);

            //switch to selected item opened in new tab
            homePage.SwitchToTabByIndex(2);

            //increase item size to 2
            ItemPage itemPage = new ItemPage(driver);
            itemPage.SelectItemSize("2");

            //Get the price of first in order to calculate the final sum in cart
            int price1 = itemPage.Getprice();
            int additionalPrice = price1 * 2;

            //Add item to cart
            itemPage.AddToCart();
            itemPage.SwitchToTabByIndex(1);

            //Select second item
            homePage.SelectItem(listOfItems[2]);
            homePage.SwitchToTabByIndex(3);

            //Get the price of second item in order to calculate the final sum in cart
            int price2 = itemPage.Getprice();
            itemPage.AddToCart();

            //calculate total
            int sum = additionalPrice + price2;

            //Switch to main page
            itemPage.SwitchToTabByIndex(1);

            homePage.Refresh();
            //Click on Cart option
            homePage.CLickOnCart();

            //Go to cart
            int totalIncart = homePage.GetTotalOfCart();
            Assert.AreEqual(totalIncart, sum);
        }

        [Description("CAPTURE IMAGES and Make it Fail")]
        [TestMethod]
        public void VerifyCartValue_Fail ()
        {

            //Select Mobiles from quick link bar  and selected samsung brand
            HomePage homePage = new HomePage(driver);
            homePage.ClickLinkFromBar(HomePage.QuickBarLinks.Mobiles);

            //Get cart count which should be 0 for new launch for freemium user
            int count = homePage.GetCartCount();
            if(count == 0)
            {
                test.AddScreenCaptureFromPath(captureScreenShot(driver, "Cart Count"));
                test.Log(AventStack.ExtentReports.Status.Fail, "Test Failed");
                Assert.Fail();
            }
            else
            {
                test.AddScreenCaptureFromPath(captureScreenShot(driver, "Cart Count"));
                test.Log(AventStack.ExtentReports.Status.Pass, "Test Passed");
            }
           
        }

        
    }
}

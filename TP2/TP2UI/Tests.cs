using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TP2UI.PageObject;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace TP2UI
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
                .ApkFile(@"c:\temp\com.companyname.appname-Signed.apk")
                .StartApp();
        }

        [Test]
        public void MainPage_SumbitRegionIsWorking_DisplayWeatherPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitRegion("quebec");

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Quebec");
            Assert.IsTrue(NameDisplayed);
        }

        [Test]
        public void LoginPage_LoginToUserIsWorking_DisplayFavoriteRegionPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitLogin("123", "456");

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Page des regions favorites");
            Assert.IsTrue(NameDisplayed);
        }

        [Test]
        public void FavoriteRegionPage_LogoutUserIsWorking_DisplayMainPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitLogin("123", "456");
            weatherPageObject.LogoutUser();

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Courriel :");
            Assert.IsTrue(NameDisplayed);
        }
    }
}

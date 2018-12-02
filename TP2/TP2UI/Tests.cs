﻿using System;
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
        public void MainPage_SubmitRegionIsWorking_DisplayWeatherPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitRegion("quebec");

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Quebec");
            Assert.IsTrue(NameDisplayed);
        }

        [Test]
        public void RegisterPage_RegisterUserIsWorking_DisplayMainPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitRegister("exemple@gmail.com", "Testtest12");

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Courriel :");
            Assert.IsTrue(NameDisplayed);
        }

        [Test]
        public void LoginPage_LoginToUserIsWorking_DisplayFavoriteRegionPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitLogin("123", "456");

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Vos favoris");
            Assert.IsTrue(NameDisplayed);
        }

        [Test]
        public void UserPage_LogoutUserIsWorking_DisplayMainPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitLogin("123", "456");
            weatherPageObject.LogoutUser();

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Courriel :");
            Assert.IsTrue(NameDisplayed);
        }

        [Test]
        public void FavoriteRegionPage_FavoriteRegionPageIsWorking_DisplayFavoriteRegionPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitLogin("123", "456");
            weatherPageObject.FavoriteRegionPage();

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Page des regions favorites");
            Assert.IsTrue(NameDisplayed);
        }

        [Test]
        public void FavoriteRegionPage_AddRegionIsWorking_DisplayFavoriteRegionPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitLogin("123", "456");
            weatherPageObject.SubmitFavoriteRegion("quebec");

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Page des regions favorites");
            Assert.IsTrue(NameDisplayed);
        }

        [Test]
        public void FavoriteRegionPage_FavoriteRegionDetailsIsWorking_DisplayWeatherPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitLogin("123", "456");
            weatherPageObject.SubmitFavoriteRegion("quebec");
            weatherPageObject.FavoriteRegion();

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Quebec");
            Assert.IsTrue(NameDisplayed);
        }
    }
}

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
        public void LoginPage_LoginToUserIsWorking_DisplayFavoriteRegionPage()
        {
            var weatherPageObject = new WeatherPageObject(app);

            weatherPageObject.SubmitLogin("123", "456");

            var NameDisplayed = weatherPageObject.IsNameDisplayed("Page des favories");
            Assert.IsTrue(NameDisplayed);
        }
    }
}

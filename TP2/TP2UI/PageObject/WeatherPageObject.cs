using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2UI.Helpers;
using Xamarin.UITest;

namespace TP2UI.PageObject
{
    class WeatherPageObject : BasePageObject
    {
        const string LOGIN_BUTTON = "LoginButton";
        const string LOGOUT_BUTTON = "LogoutButton";
        const string REGION_ENTRY = "RegionEntry";
        const string EMAILLOGIN_ENTRY = "EmailLoginEntry";
        const string PASSWORDLOGIN_ENTRY = "PasswordLoginEntry";
        const string VALIDATION_BUTTON_REGION = "ValidationButtonRegion";
        const string VALIDATION_BUTTON_LOGIN = "ValidationButtonLogin";

        public WeatherPageObject(IApp app) : base(app)
        {
        }

        public void SubmitRegion(string region)
        {
            App.WaitForElement(REGION_ENTRY);
            App.EnterText(REGION_ENTRY, region.ToString());
            App.Back();
            App.Tap(VALIDATION_BUTTON_REGION);
        }

        public void SubmitLogin(string email, string password)
        {
            App.Tap(LOGIN_BUTTON);
            App.WaitForElement(EMAILLOGIN_ENTRY);
            App.EnterText(EMAILLOGIN_ENTRY, email.ToString());
            App.WaitForElement(PASSWORDLOGIN_ENTRY);
            App.EnterText(PASSWORDLOGIN_ENTRY, password.ToString());
            App.Back();
            App.Tap(VALIDATION_BUTTON_LOGIN);
        }

        public void LogoutUser()
        {
            App.Tap(LOGOUT_BUTTON);
        }

        public bool IsNameDisplayed(string nameToCheck)
        {
            var textDisplayed = UiTestHelpers.IsTextDisplayed(App, nameToCheck);
            return textDisplayed;
        }
    }
}

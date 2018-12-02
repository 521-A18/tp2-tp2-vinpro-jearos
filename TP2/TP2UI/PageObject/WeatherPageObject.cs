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
        const string REGISTER_BUTTON = "RegisterButton";
        const string LOGOUT_BUTTON = "LogoutButton";
        const string FAVORITEREGION_BUTTON = "FavoriteRegionButton";
        const string WEATHERPAGEFAVORITE_BUTTON = "WeatherPageFavoriteButton";
        const string FAVORITE_REGION = "FavoriteRegion";
        const string REGION_ENTRY = "RegionEntry";
        const string REGIONFAVORITE_ENTRY = "RegionFavoriteEntry";
        const string EMAILREGISTER_ENTRY = "EmailRegisterEntry";
        const string PASSWORDREGISTER_ENTRY = "PasswordRegisterEntry";
        const string CONFIRMPASSWORDREGISTER_ENTRY = "ConfirmPasswordRegisterEntry";
        const string EMAILLOGIN_ENTRY = "EmailLoginEntry";
        const string PASSWORDLOGIN_ENTRY = "PasswordLoginEntry";
        const string VALIDATION_BUTTON_REGION = "ValidationButtonRegion";
        const string VALIDATION_BUTTON_REGISTER = "ValidationButtonRegister";
        const string VALIDATION_BUTTON_LOGIN = "ValidationButtonLogin";
        const string VALIDATION_BUTTON_REGIONFAVORITE = "ValidationButtonRegionFavorite";

        public WeatherPageObject(IApp app) : base(app)
        {
        }

        public void SubmitRegister(string email, string password)
        {
            App.Tap(REGISTER_BUTTON);
            App.WaitForElement(EMAILREGISTER_ENTRY);
            App.EnterText(EMAILREGISTER_ENTRY, email.ToString());
            App.Back();
            App.WaitForElement(PASSWORDREGISTER_ENTRY);
            App.EnterText(PASSWORDREGISTER_ENTRY, password.ToString());
            App.Back();
            App.WaitForElement(CONFIRMPASSWORDREGISTER_ENTRY);
            App.EnterText(CONFIRMPASSWORDREGISTER_ENTRY, password.ToString());
            App.Back();
            App.ScrollDownTo(VALIDATION_BUTTON_REGISTER);
            App.Tap(VALIDATION_BUTTON_REGISTER);
        }

        public void SubmitRegion(string region)
        {
            App.WaitForElement(REGION_ENTRY);
            App.EnterText(REGION_ENTRY, region.ToString());
            App.Back();
            App.Tap(VALIDATION_BUTTON_REGION);
        }

        public void SubmitFavoriteRegion(string region)
        {
            App.WaitForElement(REGIONFAVORITE_ENTRY);
            App.EnterText(REGIONFAVORITE_ENTRY, region.ToString());
            App.Back();
            App.Tap(VALIDATION_BUTTON_REGIONFAVORITE);
            App.Tap(WEATHERPAGEFAVORITE_BUTTON);
        }

        public void FavoriteRegion()
        {
            App.Tap(FAVORITE_REGION);
        }

        public void SubmitLogin(string email, string password)
        {
            App.WaitForElement(EMAILLOGIN_ENTRY);
            App.EnterText(EMAILLOGIN_ENTRY, email.ToString());
            App.Back();
            App.WaitForElement(PASSWORDLOGIN_ENTRY);
            App.EnterText(PASSWORDLOGIN_ENTRY, password.ToString());
            App.Back();
            App.ScrollDownTo(VALIDATION_BUTTON_LOGIN);
            App.Tap(VALIDATION_BUTTON_LOGIN);
        }

        public void LogoutUser()
        {
            App.Tap(LOGOUT_BUTTON);
        }

        public void FavoriteRegionPage()
        {
            App.Tap(FAVORITEREGION_BUTTON);
        }

        public bool IsNameDisplayed(string nameToCheck)
        {
            var textDisplayed = UiTestHelpers.IsTextDisplayed(App, nameToCheck);
            return textDisplayed;
        }
    }
}

using Xamarin.UITest;

namespace TP2UI.PageObject
{
    public class BasePageObject
    {
        protected IApp App;

        public BasePageObject(IApp app)
        {
            App = app;
        }
    }
}

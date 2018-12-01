using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace TP2UI.Helpers
{
    public static class UiTestHelpers
    {
        public static bool IsTextDisplayed(IApp app, string text)
        {
            try
            {
                app.WaitForElement(text);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

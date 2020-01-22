using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Localization.Droid.Services;
using Localization.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace Localization.Droid.Services
{
    public class Localize : ILocalization
    {
        /// <summary>
        /// Get the preferred culture of the android device
        /// </summary>
        /// <returns>Returns the preferred android culture if the culture could't be loaded the language will be set to english</returns>
        public CultureInfo GetPreferredLanguage()
        {
            var androidLocale = Locale.Default;
            var language = androidLocale.ToString().Replace("_", "-");

            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            if (!cultures.Any(c => c.Name == language))
                language = "en";

            return new CultureInfo(language);
        }

        /// <summary>
        /// Set the culture of the current thread to the culture of the android device
        /// </summary>
        public void SetLanguage()
        {
            var culture = GetPreferredLanguage();
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
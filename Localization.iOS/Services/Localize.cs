using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using Localization.iOS.Services;
using Localization.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace Localization.iOS.Services
{
    public class Localize : ILocalization
    {
        /// <summary>
        /// Get the current preferred language from the ios device.
        /// </summary>
        /// <returns>The cultueInfo with the preferred language from the ios device, 
        /// fallback vlaue if the locale could't be loaded is the basic language for 
        /// the returned prefix or finally english if nothing could be loaded </returns>
        public CultureInfo GetPreferredLanguage()
        {
            var dotnetLanguage = "en";
            var languagePrefix = "en";

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var language = NSLocale.PreferredLanguages[0];
                languagePrefix = language.Substring(0, 2);

                dotnetLanguage = language.Replace("_", "-");
            }

            CultureInfo cultInfo;
            try
            {
                cultInfo = new CultureInfo(dotnetLanguage);
            }
            catch { cultInfo = new CultureInfo(languagePrefix); }

            return cultInfo;
        }

        /// <summary>
        /// Set the culture of the current thread to the preferred culture of the ios device.
        /// </summary>
        public void SetLanguage()
        {
            var iosLocalAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
            var netLocale = iosLocalAuto.Replace("_", "-");
            CultureInfo cultInfo;
            try
            {
                cultInfo = new CultureInfo(netLocale);
            }
            catch { cultInfo = GetPreferredLanguage(); }

            Thread.CurrentThread.CurrentCulture = cultInfo;
            Thread.CurrentThread.CurrentUICulture = cultInfo;
        }
    }
}
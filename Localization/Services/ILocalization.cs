using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Localization.Services
{
    public interface ILocalization
    {
        /// <summary>
        /// Get the preferred device lanugae
        /// </summary>
        /// <returns>Returns the preferred device language (if could't be loaded just the language prefix)</returns>
        CultureInfo GetPreferredLanguage();

        /// <summary>
        /// Set the language of the current thread
        /// </summary>
        void SetLanguage();
    }
}

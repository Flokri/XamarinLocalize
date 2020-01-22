using Localization.Services;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Localization.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        #region instances
        readonly CultureInfo _culture;
        const string ResourceId = "Localization.Languages.AppResource";

        private static readonly Lazy<ResourceManager> _ressources = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));
        #endregion

        #region constructor
        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
                _culture = DependencyService.Get<ILocalization>().GetPreferredLanguage();
        }
        #endregion

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            var translation = _ressources.Value.GetString(Text, _culture);

            if (translation == null)
            {
                translation = Text;
            }

            return translation;
        }

        #region props
        public string Text { get; set; }
        #endregion
    }
}

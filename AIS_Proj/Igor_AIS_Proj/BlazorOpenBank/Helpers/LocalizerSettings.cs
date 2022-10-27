using BlazorOpenBank.Data.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;

namespace BlazorOpenBank.Helpers
{
    public static class LocalizerSettings
    {
        public const string NeutralCulture = "en-US";

        public static readonly string[] SupportedCultures = { NeutralCulture, "es-ES" };

        public static readonly (string, string)[] SupportedCulturesWithName = new[] { ("English", NeutralCulture), ("Spanish", "es-ES") };
    }
}

using BlazorOpenBank.Data.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;

namespace BlazorOpenBank.Helpers
{
    public static class Extensions
    {
        
        public async static Task SetDefaultCulture(this WebApplication host)
        {
            var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
            var cultureFromLS = await localStorage.GetItem<string>("culture");

            CultureInfo culture;
            if (cultureFromLS != null)
            {
                culture = new CultureInfo(cultureFromLS);
            }
            else
            {
                culture = new CultureInfo("en-US");
            }
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}

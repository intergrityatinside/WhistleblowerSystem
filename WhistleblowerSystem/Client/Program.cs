using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.Enums;
using System.Linq;
using Microsoft.JSInterop;
using MudBlazor.Services;

namespace WhistleblowerSystem.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {            
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var currentAccountService = new CurrentAccountService(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            await currentAccountService.InitAsync();

            var formService = new FormService(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(sp => currentAccountService);
            builder.Services.AddSingleton<ICurrentAccountService>(sp => sp.GetRequiredService<CurrentAccountService>());
            builder.Services.AddSingleton( sp => formService);
            builder.Services.AddSingleton<IFormService>(sp => sp.GetRequiredService<FormService>());
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();
            var build = builder.Build();

            //set the default language
            LanguageService.Language = Language.English; 
            CultureInfo.CurrentCulture = CultureInfo.GetCultures(CultureTypes.AllCultures)
           .First(c => CultureInfo.CreateSpecificCulture(c.Name).Name == "en-US");

            // try read the actual language from the local storage
            try
            {
                var jsRuntime = build.Services.GetRequiredService<IJSRuntime>();
                string cultureCode = await jsRuntime.InvokeAsync<string>("app.getFromLocalStorage", "culture_code");
                cultureCode = string.IsNullOrEmpty(cultureCode) ? "de-DE" : cultureCode;
                CultureInfo.CurrentCulture = CultureInfo.GetCultures(CultureTypes.AllCultures)
                           .First(c => CultureInfo.CreateSpecificCulture(c.Name).Name == cultureCode);

                LanguageService.Language = cultureCode switch
                {
                    "de-DE" => Language.German,
                    "en-US" => Language.English,
                    _ => Language.English
                };
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Language setting error {ex.Message}, set the default language (en-US)");
            }

            //run the app
            await build.RunAsync();

        }
    }
}


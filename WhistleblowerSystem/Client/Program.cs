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
using WhistleblowerSystem.Shared.Provider;

namespace WhistleblowerSystem.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddLocalization();
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<CurrentAccountService>();
            builder.Services.AddSingleton<ICurrentAccountService>(sp => sp.GetRequiredService<CurrentAccountService>());
            builder.Services.AddSingleton<WhistleblowerService>();
            builder.Services.AddSingleton<IWhistleblowerService>(sp => sp.GetRequiredService<WhistleblowerService>());
            builder.Services.AddSingleton<FormService>();
            builder.Services.AddSingleton<IFormService>(sp => sp.GetRequiredService<FormService>());
            builder.Services.AddSingleton<AttachementService>();
            builder.Services.AddSingleton<IAttachementService>(sp => sp.GetRequiredService<AttachementService>());
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();
           
            //mud
            builder.Services.AddMudServices();

            //get blockchain api uri from server
            string? blockchainApiUri = await new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }.GetStringAsync("BlockChainApiUri");
            builder.Services.AddSingleton(new BlockchainApiProvider(blockchainApiUri));
            Console.WriteLine($"Blockchain api: {blockchainApiUri}");

            var build = builder.Build();
            await build.Services.GetRequiredService<CurrentAccountService>().InitAsync();

            //set the default language
            LanguageService.Language = Language.English;
            CultureInfo.CurrentCulture = new CultureInfo("en-US");  

            // try read the actual language from the local storage
            try
            {
                var jsRuntime = build.Services.GetRequiredService<IJSRuntime>();
                string cultureCode = await jsRuntime.InvokeAsync<string>("app.getFromLocalStorage", "culture_code");
                cultureCode = string.IsNullOrEmpty(cultureCode) ? "en-US" : cultureCode;
                CultureInfo.CurrentCulture = new CultureInfo(cultureCode);
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;
                CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;

                LanguageService.Language = cultureCode switch
                {
                    "de-DE" => Language.German,
                    "en-US" => Language.English,
                    _ => Language.English
                };
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Language setting error {ex.Message}, set the default language (en-US)");
            }

            //run the app
            await build.RunAsync();

        }
    }
}


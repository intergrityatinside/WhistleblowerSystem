using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WhistleblowerSystem.Client.Services;
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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(sp => currentAccountService);
            builder.Services.AddSingleton<ICurrentAccountService>(sp => sp.GetRequiredService<CurrentAccountService>());
            builder.Services.AddMudServices();
            await builder.Build().RunAsync();
        }
    }
}


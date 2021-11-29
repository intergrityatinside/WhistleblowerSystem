using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Client.Resources;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class ViewReports
    {
        private WhistleblowerDto _whistleblower = new WhistleblowerDto("", "", "");
        private bool _success;
        private string? _message;
        [Inject] HttpClient Http { get; set; } = null!;
        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] IStringLocalizer<App> L { get; set; } = null!;

        private async Task OnLogin()
        {
            try
            {
                var whistleblower = await CurrentAccountService.Login(_whistleblower);
                _success = whistleblower != null ? true : false;
            }
            catch { }

            if (_success)
            {
                NavigationManager.NavigateTo("");

            }
            else
            {
                _message = "Falsche E-Mail oder Passwort";
                StateHasChanged();
            }

        }
    }
}
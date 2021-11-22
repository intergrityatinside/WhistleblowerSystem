
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Net.Http;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Client.Services;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class Login
    {
        private UserDto _user = new UserDto(null, "", "", "", "", "");
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
                var user = await CurrentAccountService.Login(_user);
                _success = user != null ? true : false;
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

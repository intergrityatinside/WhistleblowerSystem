
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Client.Services;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class Login
    {
        private UserDto _user = new UserDto(null, "", "", "", "", "");
        private string? _message;
        [Inject] HttpClient Http { get; set; } = null!;
        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private async Task OnLogin()
        {
            try
            {
                await CurrentAccountService.Login(_user);
                NavigationManager.NavigateTo("");
            }
            catch
            {
                _message = "Falsche E-Mail oder Passwort";
                StateHasChanged();
            }
        }
    }
}

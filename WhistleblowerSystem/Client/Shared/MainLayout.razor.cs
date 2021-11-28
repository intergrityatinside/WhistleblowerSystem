using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Client.Shared
{
    public partial class MainLayout
    {
        [Inject] IJSRuntime JSRuntime { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;

        private string _userName = "";
        private UserDto? _currentUser;
        List<(int, string)> languages = new() { 
            ((int)Language.German , "Deutsch"), 
            ((int)Language.English, "English")
        };

        async Task OnLanguageChanged(ChangeEventArgs e)
        {
            var language = (Language)Convert.ToInt32(e.Value);
            string cultureCode = language switch
            {
                Language.English => "en-US",
                Language.German => "de-DE",
                _ => throw new NotImplementedException()
            };

            await JSRuntime.InvokeVoidAsync("app.setToLocalStorage", "culture_code", cultureCode);
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;


        public void DisposeAsync()
        {
            CurrentAccountService.CurrentUserChanged -= AccountService_CurrentUserChanged;
        }

        protected override void OnInitialized()
        {
            CurrentAccountService.CurrentUserChanged += AccountService_CurrentUserChanged;
            _currentUser = CurrentAccountService.GetCurrentUser();
            SetUsername(_currentUser);
            StateHasChanged();
        }

        private void AccountService_CurrentUserChanged(object? sender, CurrentUserChangedEventArgs e)
        {
            _currentUser = CurrentAccountService.GetCurrentUser();
            SetUsername(_currentUser);
            StateHasChanged();
        }

        private async Task LogoutClicked()
        {
            await CurrentAccountService.Logout();
            _currentUser = null;
            NavigationManager.NavigateTo("login");
        }

        private void SetUsername(UserDto? userDto)
        {
            _userName = userDto != null ? userDto.FirstName + " " + userDto.Name : "";
        }

    }
}

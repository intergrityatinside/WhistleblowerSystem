using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Client.Services;

namespace WhistleblowerSystem.Client.Shared
{
    public partial class MainLayout
    {
        private string _userName = "";
        private UserDto? _currentUser;

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

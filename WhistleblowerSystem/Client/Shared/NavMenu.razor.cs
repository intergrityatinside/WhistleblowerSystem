using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Client.Services;

namespace WhistleblowerSystem.Client.Shared
{
    public partial class NavMenu
    {
        [Inject] NavigationManager NavigationManager { get; set; } = null!;
        [Inject] IStringLocalizer<App> L { get; set; } = null!;
        [Inject] CurrentAccountService CurrentAccountService { get; set; } = null!;

        [Parameter] public bool IsNavMenuCollapsed { get; set; } = false;

        private string NavMenuCssClass => IsNavMenuCollapsed ? "menu-collapsed" : "menu";
        private RoleType roleType = RoleType.Undefined;


        protected override void OnInitialized()
        {
            CurrentAccountService.CurrentUserChanged += AccountService_CurrentUserChanged;
            CurrentAccountService.CurrentWhistleblowerChanged += AccountService_CurrentWhistleblowerChanged;

            roleType = CurrentAccountService.GetCurrentUser() != null ? RoleType.Company : 
                CurrentAccountService.GetCurrentWhistleblower() != null ? RoleType.Whistleblower : RoleType.Undefined;
        }

        private void AccountService_CurrentUserChanged(object? sender, CurrentUserChangedEventArgs e)
        {
            roleType = e.CurrentUser != null ? RoleType.Company : RoleType.Undefined;
            StateHasChanged();
        }

        private void AccountService_CurrentWhistleblowerChanged(object? sender, CurrentWhistleblowerChangedEventArgs e)
        {
            roleType = e.CurrentWhistleblower != null ? RoleType.Whistleblower : RoleType.Undefined;
            StateHasChanged();
        }

        private enum RoleType
        {
            Undefined = 0,
            Whistleblower = 1,
            Company = 2
        }

        private async Task LogoutClicked()
        {
            if (CurrentAccountService.GetCurrentUser() != null)
            {
                await CurrentAccountService.Logout();
                NavigationManager.NavigateTo("");
            }
        }
    }
}

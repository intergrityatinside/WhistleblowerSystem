using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Client.Services;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class ViewReports
    {
        private WhistleblowerDto _whistleblower = new WhistleblowerDto("", "", "");
        private WhistleblowerDto _loadedWhistleblower = new WhistleblowerDto("", "", "");
        private bool _success = false;
        private string? _message;
        [Inject] HttpClient Http { get; set; } = null!;
        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] IStringLocalizer<App> L { get; set; } = null!;
        
        [Inject] private IFormService FormService { get; set; } = null!;
        
        private async Task OnLogin()
        {
            try
            {
                    // deletes blanks in the FormId
                    _whistleblower.FormId = _whistleblower.FormId.Trim();
                    _loadedWhistleblower = (await CurrentAccountService.Login(_whistleblower))!;
                    _success = _loadedWhistleblower != null ? true : false;
            }
            catch {
                _success = false;
            }

            if (_success && _loadedWhistleblower?.FormId != null)
            {
                NavigationManager.NavigateTo($"/reportdetailview/{_loadedWhistleblower.FormId}");
            }
            else
            {
                _message = L["viewreports_login_error"];
                StateHasChanged();
            }

        }
    }
}
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Client.Resources;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;

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
                _loadedWhistleblower = await CurrentAccountService.Login(_whistleblower);
                _success = _loadedWhistleblower != null ? true : false;
            }
            catch {
                _success = false;
            }

            if (_success)
            {
                var loadedForm = await FormService.LoadById(_loadedWhistleblower.Id);
                var loadedFormModel = FormService.MapFormDtoToFormModel(loadedForm);
                FormService.SetCurrentFormModel(loadedFormModel);
                NavigationManager.NavigateTo("/reportsList");
            }
            else
            {
                _message = "Falsche E-Mail oder Passwort";
                StateHasChanged();
            }

        }
    }
}
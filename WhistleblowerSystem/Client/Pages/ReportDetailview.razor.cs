using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.Enums;
using WhistleblowerSystem.Shared.Models;

namespace WhistleblowerSystem.Client.Pages

{
    public partial class ReportDetailview
    {
        private FormModel? _form;
        private bool _isCompany; 
        private ViolationState _enumValue { get; set; }


        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;
        [Inject] private IFormService FormService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected override void OnInitialized()
        {
        _form = FormService.GetCurrentFormModel()!;
        _enumValue = _form.State;
        if (CurrentAccountService.GetCurrentUser() != null)
        {
            _isCompany = true;
        }
        else
        {
            _isCompany = false;

        }
        }

        private async Task SaveState()
        {
            if (_form != null) {
                _form.State = _enumValue;
                await FormService.UpdateState(_form.Id!, _enumValue);
            }
        }

        private void NavigateBack()
        {
            FormService.SetCurrentFormModel(null);
            NavigationManager.NavigateTo("/reportsList");
        }
        private void Close()
        {
            FormService.SetCurrentFormModel(null);
            NavigationManager.NavigateTo("");
        }
    }
}
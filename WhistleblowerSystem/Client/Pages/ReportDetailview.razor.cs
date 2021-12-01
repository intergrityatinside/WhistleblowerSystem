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

        private void SaveState()
        {
        }
    }
}
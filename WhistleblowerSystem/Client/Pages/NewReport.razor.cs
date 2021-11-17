using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Business.DTOs;
using System.Collections.Generic;

namespace WhistleblowerSystem.Client.Pages

{
    public partial class NewReport
    {
        private FormDto? _form;
        private List<FormFieldDto>? _formFields;

        [Inject] private IFormService FormService { get; set; } = null!;
        [Inject] NavigationManager NavigationManager { get; set; } = null!;
        [Inject] AppStateService AppStateService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            if (AppStateService.CurrentForm == null)
            {
                _form = await FormService.GetForm();
            }
            else
            {
                _form = AppStateService.CurrentForm;
            }

            if (_form != null)
            {
                _formFields = _form.FormFields;
            }


        }



        void Navigate()
        {
            if (_form != null)
            {
                AppStateService.SetForm(_form);
                NavigationManager.NavigateTo("/upload");
            }
        }
    }
}

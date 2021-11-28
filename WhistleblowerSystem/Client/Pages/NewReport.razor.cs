using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.DTOs;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Components.Routing;

namespace WhistleblowerSystem.Client.Pages

{
    public partial class NewReport : IDisposable
    {
        private FormDto? _form;
        private List<FormFieldDto>? _formFields;
        [Inject] private IFormService FormService { get; set; } = null!;
        [Inject] NavigationManager NavigationManager { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {
            _form = FormService.GetCurrentForm() != null ? FormService.GetCurrentForm() : await FormService.GetForm();
            _formFields = _form != null ? _form.FormFields : null;

            NavigationManager.LocationChanged += CheckResetForm;
        }

        private void CheckResetForm(object? sender, LocationChangedEventArgs e)
        {
            string relativePath = e.Location.Replace(NavigationManager.BaseUri, "");

            if (!relativePath.StartsWith("upload"))
            {
                FormService.SetCurrentForm(null);
                _form = null;
                _formFields = null;
                StateHasChanged();
            }
        }

        void Navigate()
        {
            if (_form != null)
            {
                FormService.SetCurrentForm(_form);
                NavigationManager.NavigateTo("/upload");
            }
        }

        void IDisposable.Dispose()
        {
            NavigationManager.LocationChanged -= CheckResetForm;
        }
    }
}

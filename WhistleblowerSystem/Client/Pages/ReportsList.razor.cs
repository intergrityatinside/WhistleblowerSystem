using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.Models;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class ReportsList
    {
        private string? _searchString = "";
        private List<FormDto> _allForms = new();
        private List<FormModel> _allFormModels = new();
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IFormService FormService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            var loadedForms = await FormService.LoadAll();
            if (loadedForms != null)
            {
                _allForms = loadedForms;
                foreach (FormDto form in _allForms)
                {
                    var formModel = FormService.MapFormDtoToFormModel(form);
                    _allFormModels.Add(formModel);
                }
            }
            else
            {
                Console.Write("no forms");
            }
        }

        private bool FilterFunc1(FormModel formModel) => FilterFunc(formModel, _searchString);

        private bool FilterFunc(FormModel formModel, string? searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return true;
            }

            if (formModel.Id != null)
            {
                if (formModel.Id.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void FormSelected(TableRowClickEventArgs<FormModel> tableRowClickEventArgs)
        {
            FormService.SetCurrentFormModel(tableRowClickEventArgs.Item);
            NavigationManager.NavigateTo("/reportdetailview");
        }
    }
}
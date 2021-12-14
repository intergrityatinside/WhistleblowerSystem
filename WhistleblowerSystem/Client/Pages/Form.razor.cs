using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class Form
    {
        [Inject] private IFormService FormService { get; set; } = null!;
        [Inject] private IDialogService? DialogService { get; set; }
        [Inject] private IAttachementService AttachementService { get; set; } = null!;
        [Inject] NavigationManager NavigationManager { get; set; } = null!;

        private int _step = 0; // 0 = security notice, 1 = form, 2 = upload, 3 = confirmation
        private bool CheckedValue { get; set; } = false;
        private FormDto? _form;
        private List<FormFieldDto>? _formFields;
        private MudForm _mudForm = new MudForm();
        private string? _dragEnterStyle;
        bool IsTaskRunning = false;
        bool rerender = false;


        private async Task OpenForm()
        {
            _form = await FormService.GetForm();
            _formFields = _form != null ? _form.FormFields : null;
            _step = 1;
        }

        private async Task SubmitForm()
        {
            await _mudForm.Validate();
            if (_mudForm.IsValid)
            {
                _step = 2;
            }
        }

        private void BackToForm()
        {
            _step = 1;
        }


        private void CloseForm()
        {
            _step = 0;
            NavigationManager.NavigateTo("/home");
        }


        protected override bool ShouldRender()
        {
            var render = base.ShouldRender() || rerender;
            rerender = false;
            return render;
        }


        private async Task OnInputFileChanged(InputFileChangeEventArgs e)
        {
            var addedFiles = e.GetMultipleFiles().ToList();
            bool fileExistsWithSameName = false;
            if (addedFiles != null)
            {
                foreach (var addedFile in addedFiles) {
                    if (_form != null && _form.Attachements != null)
                    {
                        foreach (var file in _form!.Attachements!)
                        {
                            if (file.Filename == addedFile.Name)
                            {
                                await DialogService!.ShowMessageBox(
                                    L["upload_error_title"],
                                    L["upload_error_message"],
                                    yesText:L["upload_error_yestext"]);
                                fileExistsWithSameName = true;
                                break;
                            }
                        }
                    }

                    if (fileExistsWithSameName)
                    {
                        continue;
                    }

                    var attachementMetaData = await AttachementService.Save(addedFile);
                    if (attachementMetaData != null)
                    {
                        _form!.Attachements = _form!.Attachements ?? new List<AttachementMetaDataDto>();
                        _form!.Attachements.Add(attachementMetaData);
                    }
                    rerender = true;
                }
            }
        }

        private async Task UploadDocs()
        {
            IsTaskRunning = true;

            FormDto? savedForm = await FormService.Save(_form!);
            if (savedForm != null)
            {
                _form = savedForm;
                Console.WriteLine("Form saved");
                _step = 3;
            }
        }

        private async Task DeleteDocument(string id, string filename)
        {
            await AttachementService.Delete(id);
            _form!.Attachements!.Remove(_form!.Attachements.First(a => a.AttachementId == id));
        }
    }
}
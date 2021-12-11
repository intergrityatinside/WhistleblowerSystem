using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.Enums;
using WhistleblowerSystem.Shared.Models;

namespace WhistleblowerSystem.Client.Pages

{
    public partial class ReportDetailview : IDisposable
    {
        private FormModel? _form;
        private FormMessageDto? _formMessageDto;
        private bool _isCompany;
        private ViolationState _enumValue { get; set; }
        bool rerender = false;


        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;
        [Inject] private IFormService FormService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IAttachementService AttachementService { get; set; } = null!;

        protected override void OnInitialized()
        {
            _formMessageDto = new FormMessageDto(null, "", CurrentAccountService.GetCurrentUser()!, DateTime.Now);
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
            if (_form != null)
            {
                _form.State = _enumValue;
                await FormService.UpdateState(_form.Id!, _enumValue);
            }

            _form!.State = _enumValue;
            await FormService.UpdateState(_form.Id!, _enumValue);
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

        private async Task SendMessage()
        {
            _form!.Messages!.Add(_formMessageDto!);
            await FormService.AddMessage(_form.Id!, _formMessageDto!);
            _formMessageDto = new FormMessageDto(null, "", CurrentAccountService.GetCurrentUser()!, DateTime.Now);
            StateHasChanged();
        }

        private async Task AttachFiles()
        {
            _form!.Messages!.Add(_formMessageDto!);
            await FormService.AddMessage(_form.Id!, _formMessageDto!);
            _formMessageDto = new FormMessageDto(null, "", CurrentAccountService.GetCurrentUser()!, DateTime.Now);
            StateHasChanged();
        }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            var addedFiles = e.GetMultipleFiles().ToList();
            bool fileExistsWithSameName = false;
            if (addedFiles != null)
            {
                foreach (var addedFile in addedFiles)
                {
                    if (_form != null && _form.Attachements != null)
                    {
                        foreach (var file in _form!.Attachements!)
                        {
                            if (file.Filename == addedFile.Name)
                            {
                                ////TODO: Mehrsprachig
                                //await DialogService.ShowMessageBox(
                                //    "Warnung",
                                //    "Es wurde bereits ein File mit dem selben Namen hinzugefügt",
                                //    yesText: "Schliessen");
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
                        await FormService.AddFile(_form!.Id!, attachementMetaData!);
                        rerender = true;
                    }
                }
            }
        }

        private string GetMessageStyle(FormMessageDto messageDto)
        {
            //check if message is from current user
            bool myMessage = _isCompany && messageDto.User != null || !_isCompany && messageDto.User == null;

            string messageStyle = "width: 70%;";
            return myMessage
                ? messageStyle + "text-align: right;background-color: #555455; color: white;"
                : messageStyle;
        }

        private string GetSender(FormMessageDto messageDto)
        {
            var sender = (messageDto.User != null) ? "Sachbearbeiter" : "Melder";
            bool myMessage = _isCompany && messageDto.User != null || !_isCompany && messageDto.User == null;
            return myMessage ? "Ich" : sender;
        }

        private string GetPosition(FormMessageDto messageDto)
        {
            bool myMessage = _isCompany && messageDto.User != null || !_isCompany && messageDto.User == null;
            return myMessage ? "flex-direction: row-reverse;" : "flex-direction: row;";
        }

        void IDisposable.Dispose()
        {
            FormService.SetCurrentFormModel(null);
        }
        protected override bool ShouldRender()
        {
            var render = base.ShouldRender() || rerender;
            rerender = false;
            return render;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using MudBlazor;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.Enums;
using WhistleblowerSystem.Shared.Models;

namespace WhistleblowerSystem.Client.Pages

{
    public partial class ReportDetailview : IDisposable
    {
        [Parameter] public string CaseId { get; set; } = string.Empty;
        private FormModel? _form;
        private FormMessageDto? _formMessageDto;
        private bool _isCompany;
        private ViolationState _enumValue { get; set; }
        bool rerender = false;


        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;
        [Inject] private IFormService FormService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IAttachementService AttachementService { get; set; } = null!;
        [Inject] private IDialogService? DialogService { get; set; }

        [Inject] IStringLocalizer<App> L { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            NavigationManager.LocationChanged += UrlChanged;
            _isCompany = CurrentAccountService.GetCurrentUser() != null ? true : false;
            _formMessageDto = new FormMessageDto(null, "", CurrentAccountService.GetCurrentUser()!, DateTime.Now);
            var result = await FormService.LoadById(CaseId!);
            if (result != null)
            {
                _form = FormService.MapFormDtoToFormModel(result!);
                //check if states needs to be updated
                await CheckUpdateState();
                //set dropdown value of current state
                _enumValue = _form!.State;
            }
            else
            {
                // can't find report, returns to viewreport page or reportlist
                var link = _isCompany ? "/reportslist" : "/viewreports";
                NavigationManager.NavigateTo(link);
            }
        }

        private async void UrlChanged(object? sender, LocationChangedEventArgs e)
        {
            string relativePath = e.Location.Replace(NavigationManager.BaseUri, "");
            if (!relativePath.StartsWith("reportdetailview"))
            {
                if (CurrentAccountService.GetCurrentWhistleblower() != null)
                {
                    await LogoutClicked();
                    NavigationManager.NavigateTo(relativePath);
                }
            }
        }

        private async Task CheckUpdateState()
        {
            if (_isCompany && _form!.State.Equals(ViolationState.Undefined))
            {
                _form.State = ViolationState.Received;
                await FormService.UpdateState(_form.Id!, _form.State);
                StateHasChanged();
            }
        }

        private async Task SaveState()
        {
            if (_form != null)
            {
                _form.State = _enumValue;
                await FormService.UpdateState(_form.Id!, _enumValue);
            }
        }

        private void NavigateBack()
        {
            NavigationManager.NavigateTo("/reportsList");
        }

        private void Close()
        {
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
                                await DialogService!.ShowMessageBox(
                                    L["reportdetailview_upload_warning"],
                                    L["reportdetailview_upload_warning_text"],
                                    yesText: L["reportdetailview_upload_warning_close"]);
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
            var sender = (messageDto.User != null)
                ? "reportdetailview_sender_clerk"
                : "reportdetailview_sender_reporter";
            bool myMessage = _isCompany && messageDto.User != null || !_isCompany && messageDto.User == null;
            return myMessage ? "reportdetailview_sender_you" : sender;
        }

        private string GetPosition(FormMessageDto messageDto)
        {
            bool myMessage = _isCompany && messageDto.User != null || !_isCompany && messageDto.User == null;
            return myMessage ? "flex-direction: row-reverse;" : "flex-direction: row;";
        }

        void IDisposable.Dispose()
        {
            // TODO
        }

        protected override bool ShouldRender()
        {
            var render = base.ShouldRender() || rerender;
            rerender = false;
            return render;
        }

        private async Task LogoutClicked()
        {
            if (CurrentAccountService.GetCurrentWhistleblower() != null)
            {
                await CurrentAccountService.Logout();
                NavigationManager.NavigateTo("");
            }
        }
    }
}
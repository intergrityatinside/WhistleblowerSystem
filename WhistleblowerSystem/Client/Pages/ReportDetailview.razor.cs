using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.Enums;
using WhistleblowerSystem.Shared.Models;

namespace WhistleblowerSystem.Client.Pages

{
    public partial class ReportDetailview : IDisposable
    {
        [Parameter] public string CaseId { get; set; } = null;
        private FormModel? _form;
        private FormMessageDto? _formMessageDto;
        private bool _isCompany;
        private ViolationState _enumValue;


        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;
        [Inject] private IFormService FormService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            _isCompany = CurrentAccountService.GetCurrentUser() != null ? true : false;
            _formMessageDto = new FormMessageDto(null, "", CurrentAccountService.GetCurrentUser()!, DateTime.Now);
            var result = await FormService.LoadById(CaseId!);
            if (result != null){
                _form = FormService.MapFormDtoToFormModel(result!);
                _enumValue = _form!.State;
            }
            else
            {
                // can't find report, returns to viewreport page or reportlist
                var link = _isCompany ? "/reportslist" : "/viewreports";
                NavigationManager.NavigateTo(link);
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
    }
}
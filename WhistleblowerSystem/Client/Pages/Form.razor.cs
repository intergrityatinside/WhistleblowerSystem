using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Client.Services;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class Form
    {
        //private FormDto? _form;
        private FormTemplateDto? _formTemplate;
        //private string? _message;
        [Inject] HttpClient Http { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            _formTemplate = await Http.GetFromJsonAsync<FormTemplateDto>("formTemplate");
        }

        //private async Task OnLogin()
        //{
        //    try
        //    {
        //        await CurrentAccountService.Login(_user);
        //        NavigationManager.NavigateTo("");
        //    }
        //    catch
        //    {
        //        _message = "Falsche E-Mail oder Passwort";
        //        StateHasChanged();
        //    }
        //}
    }
}

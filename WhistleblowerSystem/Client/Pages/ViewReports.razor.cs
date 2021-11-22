using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Client.Services;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class ViewReports
    {
        private WhistleblowerDto _whistleblower = new WhistleblowerDto("", "", "");
       // private bool _success;
       // private string? _message;
        [Inject] HttpClient Http { get; set; } = null!;
        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] IStringLocalizer<App> L { get; set; } = null!;
    }
}
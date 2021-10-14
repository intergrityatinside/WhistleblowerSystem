using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class Register
    {
        private UserDto _user = new UserDto(null, "", "", "","","");
        private List<CompanyDto>? _companies;

        [Inject] HttpClient Http { get; set; } = null!;
        [Inject] NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
       _companies = await Http.GetFromJsonAsync<List<CompanyDto>>("company");
    }

    private async Task OnRegister()
    {
        await Http.PostAsJsonAsync("User", _user);
        NavigationManager.NavigateTo("url to next page");
    }
}
}

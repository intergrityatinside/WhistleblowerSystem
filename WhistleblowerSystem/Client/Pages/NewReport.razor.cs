using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using WhistleblowerSystem.Client.Services;

namespace WhistleblowerSystem.Client.Pages

{
    public partial class NewReport
    {

        [Inject] private IFormService FormService { get; set; } = null!;


    protected override async Task OnInitializedAsync()
    {
        await FormService.GetForm();
    }

    }
}

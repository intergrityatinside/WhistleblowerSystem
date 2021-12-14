using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Client.Services;
using System.Globalization;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace WhistleblowerSystem.Client.Pages
{
    public partial class Index
    {
        [Inject] IJSRuntime JSRuntime { get; set; } = null!;
        private bool _showDisclaimer;


        protected async override Task OnInitializedAsync()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            string cultureCode = await JSRuntime.InvokeAsync<string>("app.getFromLocalStorage", "culture_code");
            cultureCode = string.IsNullOrEmpty(cultureCode) ? "en-US" : cultureCode;
            CultureInfo.CurrentCulture = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;

            switch (cultureCode)
            {
                case "de-DE":
                    _showDisclaimer = !(await JSRuntime.InvokeAsync<string>("app.getFromLocalStorage", "disclaimer_showed_de") == "true");
                    await JSRuntime.InvokeVoidAsync("app.setToLocalStorage", "disclaimer_showed_de", "true");
                    break;

                case "en-US":
                    _showDisclaimer = !(await JSRuntime.InvokeAsync<string>("app.getFromLocalStorage", "disclaimer_showed_en") == "true");
                    await JSRuntime.InvokeVoidAsync("app.setToLocalStorage", "disclaimer_showed_en", "true");
                    break;

                default:
                    _showDisclaimer = !(await JSRuntime.InvokeAsync<string>("app.getFromLocalStorage", "disclaimer_showed_en") == "true");
                    await JSRuntime.InvokeVoidAsync("app.setToLocalStorage", "disclaimer_showed_en", "true");
                    break;
            }

            StateHasChanged();
        }

        private void ToggleOpen()
        {
            _showDisclaimer = false;
        }
    }
}

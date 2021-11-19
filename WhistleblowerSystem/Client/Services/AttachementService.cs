using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public class AttachementService : IAttachementService
    {
        private readonly HttpClient _http;
        private AttachementMetaDataDto? _currentAttachementMetaData;

        public AttachementService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AttachementMetaDataDto?> Save(IBrowserFile file)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("Attachement", file);
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {
                Console.WriteLine(response);
                _currentAttachementMetaData = await response.Content.ReadFromJsonAsync<AttachementMetaDataDto>();
            }
            return _currentAttachementMetaData;
        }
    }
}

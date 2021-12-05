using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;

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
            using var content = new MultipartFormDataContent();
            var fileContent =
                        new StreamContent(file.OpenReadStream(long.MaxValue));

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(file.ContentType);

            content.Add(
                content: fileContent,
                name: "\"files\"",
                fileName: file.Name);

            HttpResponseMessage? response = await _http.PostAsync("Attachement", content);

            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {
                Console.WriteLine(response);
                _currentAttachementMetaData = await response.Content.ReadFromJsonAsync<AttachementMetaDataDto>();
            }
            return _currentAttachementMetaData;
        }

        public async Task Delete(string id) {
            HttpResponseMessage? response = await _http.DeleteAsync($"Attachement/{id}");

            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {
                Console.WriteLine(response);
            }
        }
    }
}

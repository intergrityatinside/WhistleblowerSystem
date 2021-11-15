using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public class WhistleblowerService: IWhistleblowerService
    {
        private readonly HttpClient _http;
        private WhistleblowerDto? _whistleblower;

        public WhistleblowerService(HttpClient http)
        {
            _http = http;
        }

        public async Task<WhistleblowerDto?> Save(WhistleblowerDto whistleblower)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("Whistleblower", whistleblower);
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {
                Console.WriteLine(response);
                _whistleblower = await response.Content.ReadFromJsonAsync<WhistleblowerDto>();
            }
            return _whistleblower;
        }
    }
}

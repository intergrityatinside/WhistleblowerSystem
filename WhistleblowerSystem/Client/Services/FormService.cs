using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public class FormService : IFormService
    {
        private readonly HttpClient _http;
        private FormDto? _currentForm;

        public FormService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetForm()
        {
            var response = await _http.GetAsync("Form");
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {

                _currentForm = await response.Content.ReadFromJsonAsync<FormDto>();
            }
        }

        public Task Load(FormDto formDto)
        {
            throw new NotImplementedException();
        }

        public Task<FormDto> Save()
        {
            throw new NotImplementedException();
        }

        //public async Task Load(FormDto formDto)
        //{
        //    var response = await _http.PostAsJsonAsync("Authentication/login", userDto);
        //    _currentForm = !response.IsSuccessStatusCode ? null : await response.Content.ReadFromJsonAsync<UserDto?>();
        //    CurrentUserChanged?.Invoke(this, new CurrentUserChangedEventArgs(_currentUser));
        //}

        //public async Task<FormDto> Save()
        //{
        //    await _http.PostAsJsonAsync("Authentication/logout", _currentUser);
        //    _currentUser = null;
        //    CurrentUserChanged?.Invoke(this, new CurrentUserChangedEventArgs(null));
        //}

    }
}

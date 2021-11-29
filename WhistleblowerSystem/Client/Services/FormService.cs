using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public class FormService : IFormService
    {
        private readonly HttpClient _http;
        private FormDto? _currentForm;
        private List<FormDto>? _allForms;

        public FormService(HttpClient http)
        {
            _http = http;
        }

        public async Task<FormDto?> GetForm()
        {
            HttpResponseMessage? response = await _http.GetAsync("Form");
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {
                _currentForm = await response.Content.ReadFromJsonAsync<FormDto>();
            }
            return _currentForm;
        }

        public Task Load(FormDto formDto)
        {
            throw new NotImplementedException();
        }
        
        public async Task <List<FormDto>?> LoadAll()
        {
            HttpResponseMessage? response = await _http.GetAsync("Form/getAll");
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            { 
                _allForms = await response.Content.ReadFromJsonAsync<List<FormDto>>();
            }
            return _allForms;        
        }

        public async Task<FormDto?> Save(FormDto formDto)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("Form/save", formDto);
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {
                _currentForm = await response.Content.ReadFromJsonAsync<FormDto>();
            }
            return _currentForm;
        }

        public void SetCurrentForm(FormDto? form)
        {
            _currentForm = form ?? null;
        }

        public FormDto? GetCurrentForm()
        {
            return _currentForm;
        }
    }
}

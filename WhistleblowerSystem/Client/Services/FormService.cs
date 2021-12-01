using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.Enums;
using WhistleblowerSystem.Shared.Models;

namespace WhistleblowerSystem.Client.Services
{
    public class FormService : IFormService
    {
        private readonly HttpClient _http;
        private FormDto? _currentForm;
        private FormModel? _currentFormModel;
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

        public async Task<FormDto?> LoadById(string id) {
            HttpResponseMessage? response = await _http.GetAsync($"Form/{id}");
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {
                _currentForm = await response.Content.ReadFromJsonAsync<FormDto>();
            }
            return _currentForm;
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
        
        public async Task UpdateState(string id, ViolationState state)
        {
            await _http.PostAsJsonAsync("Form/"+id+"/changeState",state);
        }

        public void SetCurrentForm(FormDto? form)
        {
            _currentForm = form ?? null;
        }

        public FormDto? GetCurrentForm()
        {
            return _currentForm;
        }

        public void SetCurrentFormModel(FormModel? form)
        {
            _currentFormModel = form ?? null;
        }
        
        public FormModel? GetCurrentFormModel()
        {
            return _currentFormModel;
        }

        public FormModel MapFormDtoToFormModel(FormDto dto)
        {
            var state = getStateString(dto.State);
            var title = getField(dto.FormFields, "Beschreibung")?.SelectedValues[0];
            var description = getField(dto.FormFields, "Vorfall")?.SelectedValues[0];
            var formModel = new FormModel(dto.Id, dto.TopicId, dto.FormTemplateId, dto.FormFields, dto.Attachements, dto.Messages, dto.State, dto.Datetime, "", title!, description!, state);
            return formModel;
        }
        
        private FormFieldDto? getField(List<FormFieldDto> formFields, string searchString)
        {
            return formFields.Find((formField) => formField.Texts[0]?.Value == searchString);
        }

        private string getStateString(ViolationState state)
        {
            // ReSharper disable once HeapView.BoxingAllocation
            return state.ToString();
        }

    }
}

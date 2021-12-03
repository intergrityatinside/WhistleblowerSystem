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
        
        public async Task AddMessage(string formId, FormMessageDto messageDto)
        { 
            await _http.PostAsJsonAsync($"Form/{formId}/addMessage", messageDto);
        }
        
        public async Task UpdateState(string id, ViolationState state)
        {
            await _http.PostAsJsonAsync($"Form/{id}/changeState/{state}", new object());
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
            var titleField = getField(dto.FormFields, "Beschreibung");
            var title = (titleField != null && titleField.SelectedValues.Count > 0) ? titleField.SelectedValues[0] : string.Empty;
            var descriptionField = getField(dto.FormFields, "Vorfall");
            var description = (descriptionField != null && descriptionField.SelectedValues.Count > 0) ? descriptionField.SelectedValues[0] : string.Empty;
            var optionalFields = getOptionalFields(dto.FormFields);
            var formModel = new FormModel(dto.Id, dto.TopicId, dto.FormTemplateId, optionalFields, dto.Attachements, dto.Messages, dto.State, dto.Datetime, "", title!, description!, state);
            return formModel;
        }
        
        private FormFieldDto? getField(List<FormFieldDto> formFields, string searchString)
        {
            return formFields.Find((formField) => formField.Texts[0]?.Value == searchString);
        }

        private string getStateString(ViolationState state)
        {
            return state.ToString();
        }

        //todo later check isoptional bool on fielddto
        private List<FormFieldDto> getOptionalFields(List<FormFieldDto> formFields)
        {
            List<FormFieldDto> optionalFields = new List<FormFieldDto>();
            foreach (var field in formFields)
            {
                if (field != getField(formFields, "Vorfall") && field != getField(formFields, "Beschreibung"))
                {
                    optionalFields.Add(field);
                }
            }
            return optionalFields;
        }

    }
}

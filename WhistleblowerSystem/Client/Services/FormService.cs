using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Client.Utils;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.Enums;
using WhistleblowerSystem.Shared.Models;
using WhistleblowerSystem.Shared.Provider;

namespace WhistleblowerSystem.Client.Services
{
    public class FormService : IFormService
    {
        private readonly HttpClient _http;
        private FormDto? _currentForm;
        private List<FormDto>? _allForms;
        private BlockchainApiProvider _blockchainApiProvider;
        //private ReportApi _reportApi;
        public FormService(HttpClient http, BlockchainApiProvider blockchainApiProvider)
        {
            _http = http;
            _blockchainApiProvider = blockchainApiProvider;
            //_reportApi = new ReportApi(...,..., { BasePath = blockchainApiProvider.BaseUri! });
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
            //_reportApi.ReportsgetReports();
            HttpResponseMessage? response = await _http.GetAsync("Form/getAll");
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            { 
                _allForms = await response.Content.ReadFromJsonAsync<List<FormDto>>();
            }
            return _allForms;        
        }

        public async Task<FormDto?> Save(FormDto formDto)
        {
            //var titleField = getField(formDto.FormFields, "Beschreibung");
            //var title = (titleField != null && titleField.SelectedValues.Count > 0) ? titleField.SelectedValues[0] : string.Empty;
            //var descriptionField = getField(formDto.FormFields, "Vorfall");
            //var description = (descriptionField != null && descriptionField.SelectedValues.Count > 0) ? descriptionField.SelectedValues[0] : string.Empty;
            //ReportModel reportModel = new ReportModel();
            //reportModel.Title = title;
            //reportModel.Description = description;

            //Variante 1: Generierte API:

            //_reportApi.ReportspostReport(reportModel);

            //Variante 2: manuell Whistleblower Blockchain Api Call
            //var blockChainHttpClient = new HttpClient { BaseAddress = new Uri(_blockchainApiProvider.BaseUri!) };
            //await blockChainHttpClient.PostAsJsonAsync("Report", reportModel);

            //Whistleblower Api Call
            HttpResponseMessage? response = await _http.PostAsJsonAsync("Form/save", formDto);
            if (!string.IsNullOrEmpty(value: await response.Content.ReadAsStringAsync()))
            {
                _currentForm = await     response.Content.ReadFromJsonAsync<FormDto>();
            }
            return _currentForm;
        }
        
        public async Task AddMessage(string formId, FormMessageDto messageDto)
        {
            //RoleEnumModel role = messageDto.User == null ? RoleEnumModel.User : RoleEnumModel.Staff;
            //var messageModel = new MessageModel(messageDto.Text);
            //var messageObjectModel = new MessageObjectModel(role, messageModel);
            //_reportApi.ReportsAddMessageByReportId(formId, messageObjectModel);

            await _http.PostAsJsonAsync($"Form/{formId}/addMessage", messageDto);
        }

        public async Task AddFile(string formId, AttachementMetaDataDto attachementMetaDataDto)
        {
            await _http.PostAsJsonAsync($"Form/{formId}/addFile", attachementMetaDataDto);
        }

        public async Task UpdateState(string id, ViolationState state)
        {
            //_reportApi.ReportsUpdateStatusByReportId(id, GetStatusModelByState(state));
            await _http.PostAsJsonAsync($"Form/{id}/changeState/{state}", new object());
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
        
        private List<FormFieldDto> getOptionalFields(List<FormFieldDto> formFields)
        {
            List<FormFieldDto> optionalFields = new List<FormFieldDto>();
            foreach (var field in formFields)
            {
                if (field.Mandatory == false)
                {
                    if (field.SelectedValues.Count > 0) {
                        optionalFields.Add(field);
                    }
                }
            }
            return optionalFields;
        }

        //private string GetStatusModelByState(ViolationState state) {
        //    switch (state) {
        //        case ViolationState.Undefined:
        //            return StatusModel.Undefined.ToString();
        //        case ViolationState.Accepted:
        //            return StatusModel.Received.ToString();
        //        case ViolationState.InProgress:
        //            return StatusModel.Processing.ToString();
        //        case ViolationState.Done:
        //            return StatusModel.Closed.ToString();
        //        default:
        //            return StatusModel.Undefined.ToString();
        //    }
        //}

    }
}

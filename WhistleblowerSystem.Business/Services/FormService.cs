using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Repositories;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Business.Services
{
    public class FormService
    {
        readonly FormRepository _formRepository;
        readonly FormTemplateRepository _formTemplateRepository;
        readonly IMapper _mapper;

        public FormService(FormRepository formRepository, FormTemplateRepository formTemplateRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _formTemplateRepository = formTemplateRepository;
            _mapper = mapper;

        }

        public async Task<FormDto> CreateFormAsync(FormDto formDto)
        {
            Form form = _mapper.Map<Form>(formDto);
            await _formRepository.InsertOneAsync(form);
            return _mapper.Map<FormDto>(form);
        }

        public async Task<FormDto> CreateFormFromTemplateAsync()
        {
            FormTemplate formTemplate = await _formTemplateRepository.GetFirstAsync();
            var formTemplateDto = _mapper.Map<FormTemplateDto>(formTemplate);
            List<FormFieldDto> formFields = new List<FormFieldDto>();
            foreach (FormFieldTemplateDto field in formTemplateDto.Fields) {
                var selected = new List<string>();
                FormFieldDto formField = new FormFieldDto(null, field.Text, field.Type, selected, field.SelectionValues);
                formFields.Add(formField);
            }

            FormDto formDto = new FormDto(null, null, formTemplateDto.Id!, formFields, null, null, Shared.Enums.ViolationState.Undefined);
            return formDto;
        }
        public async Task<List<FormDto>> GetAllAsync()
        {
            var forms = await _formRepository.GetAllAsync();
            return _mapper.Map<List<FormDto>>(forms);
        }

        public async Task<FormDto> GetAsync(string id)
        {
            var form = await _formRepository.FindOneAsync(id);
            return _mapper.Map<FormDto>(form);
        }

        public async Task AddMessage(string id, FormMessageDto messageDto)
        {
            var message = _mapper.Map<FormMessage>(messageDto);
            await _formRepository.AddMessage(id, message);
        }

        public async Task ChangeState(string id, ViolationState state)
        {
            await _formRepository.ChangeState(id, state);
        }
    }
}

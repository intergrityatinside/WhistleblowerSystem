using AutoMapper;
using Microsoft.VisualBasic.CompilerServices;
using PasswordGenerator;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Business.Utils;
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
        readonly WhistleblowerRepository _whistleblowerRepository;

        public FormService(FormRepository formRepository, FormTemplateRepository formTemplateRepository,
           WhistleblowerRepository whistleblowerRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _formTemplateRepository = formTemplateRepository;
            _mapper = mapper;
            _whistleblowerRepository = whistleblowerRepository;
        }

        public async Task<FormDto> CreateFormAsync(FormDto formDto)
        {
            Form form = _mapper.Map<Form>(formDto);
            await _formRepository.InsertOneAsync(form);

            var formResult = _mapper.Map<FormDto>(form);
            var passwordGenerator = new Password()
                .IncludeLowercase()
                .IncludeUppercase()
                .IncludeNumeric()
                .LengthRequired(10)
                .IncludeSpecial("[]{}^_=");
            formResult.Password = passwordGenerator.Next();

            var whistleBlower = new Whistleblower(null, form.Id.ToString(), PasswordUtils.HashPw(formResult.Password));
            await _whistleblowerRepository.InsertOneAsync(whistleBlower);

            return formResult;
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

            FormDto formDto = new FormDto(null, null, formTemplateDto.Id!, formFields, null, null, Shared.Enums.ViolationState.Undefined, null);
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

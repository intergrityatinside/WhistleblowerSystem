using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Repositories;

namespace WhistleblowerSystem.Business.Services
{
    public class FormTemplateService
    {
        readonly FormTemplateRepository _formTemplateRepository;
        readonly IMapper _mapper;

        public FormTemplateService(FormTemplateRepository formTemplateRepository, IMapper mapper)
        {
            _formTemplateRepository = formTemplateRepository;
            _mapper = mapper;

        }

        public async Task<FormTemplateDto> CreateFormTemplateAsync(FormTemplateDto formTemplateDto)
        {
            FormTemplate form = _mapper.Map<FormTemplate>(formTemplateDto);
            await _formTemplateRepository.InsertOneAsync(form);
            return _mapper.Map<FormTemplateDto>(form);
        }

        public async Task<List<FormTemplateDto>> GetAllFormTemplatesAsync()
        {
            var forms = await _formTemplateRepository.GetAllAsync();
            return _mapper.Map<List<FormTemplateDto>>(forms);
        }

        public async Task<FormTemplateDto?> GetFormTemplateByIdAsync(string id)
        {
            var form = await _formTemplateRepository.FindOneAsync(id);
            return _mapper.Map<FormTemplateDto>(form);
        }
    }
}

using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Repositories;

namespace WhistleblowerSystem.Business.Services
{
    public class FormService
    {
        readonly FormRepository _formRepository;
        readonly IMapper _mapper;

        public FormService(FormRepository formRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;

        }

        public async Task<FormDto> CreateFormAsync(FormDto formDto)
        {
            Form form = _mapper.Map<Form>(formDto);
            await _formRepository.InsertOneAsync(form);
            return _mapper.Map<FormDto>(form);
        }

        public async Task<List<FormDto>> GetAllFormsAsync()
        {
            var forms = await _formRepository.GetAllAsync();
            return _mapper.Map<List<FormDto>>(forms);
        }

        public async Task<FormDto?> GetCompanyByIdAsync(string id)
        {
            var form = await _formRepository.FindOneAsync(id);
            return _mapper.Map<FormDto>(form);
        }
    }
}

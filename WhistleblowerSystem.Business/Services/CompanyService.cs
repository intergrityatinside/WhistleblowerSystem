using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Repositories;

namespace WhistleblowerSystem.Business.Services
{
    public class CompanyService
    {
        readonly CompanyRepository _companyRepository;
        readonly IMapper _mapper;

        public CompanyService(CompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;

        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto)
        {
            Company company = _mapper.Map<Company>(companyDto);
            await _companyRepository.InsertOneAsync(company);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return _mapper.Map<List<CompanyDto>>(companies);
        }

        public async Task<CompanyDto?> GetCompanyByIdAsync(string id)
        {
            var company = await _companyRepository.FindOneAsync(id);
            return _mapper.Map<CompanyDto>(company);
        }
    }
}

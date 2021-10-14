using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Server.Authentication;

namespace WhistleblowerSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : BaseController
    {
        private readonly CompanyService _companyService;

        public CompanyController(UserManager userManager,
            IHttpContextAccessor httpContextAccessor,
            CompanyService companyService) : base(httpContextAccessor, userManager)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<List<CompanyDto>> Get()
        {
            return await _companyService.GetAllCompaniesAsync();
        }
    }
}

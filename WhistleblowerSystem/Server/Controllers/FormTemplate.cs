using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [Route("[formTemplate]")]
    public class FormTemplate : BaseController
    {
        private readonly FormTemplateService _formTemplateService;
        public FormTemplate(UserManager userManager,
            IHttpContextAccessor httpContextAccessor,
            FormTemplateService formTemplateService) : base(httpContextAccessor, userManager)
        {
            _formTemplateService = formTemplateService;
        }

        //[HttpGet]
        //public async Task<List<FormTemplateDto>> Get()
        //{
        //    return await _formTemplateService.;
        //}
    }
}

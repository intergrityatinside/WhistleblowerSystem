using Microsoft.AspNetCore.Authorization;
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
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : UserBaseController
    {
        private readonly FormService _formService;
        public FormController(UserManager userManager,
            IHttpContextAccessor httpContextAccessor,
            FormService formService) : base(httpContextAccessor, userManager)
        {
            _formService = formService;
        }

        [HttpGet("get")]
        public async Task<FormDto> Get(string topicId)
        {
            return await _formService.CreateFormFromTemplateAsync();
        }

        [HttpPost("save")]
        public async Task<FormDto> Save(FormDto formDto)
        {
            var form = await _formService.CreateFormAsync(formDto);
            if (form == null) throw new Exception("Save form failed");
            return form;
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<List<FormDto>> GetAll()
        {
            return await _formService.GetAllAsync();
        }

        [HttpPost("addMessage")]
        public async Task AddMessage(UserDto user, string message)
        {
            var timeStamp = DateTime.Now;
            FormMessageDto formMessageDto = new FormMessageDto(null, message, user, timeStamp);
            await _formService.AddMessage(user.Id!, formMessageDto);
        }

        [Authorize]
        [HttpPost("changeState")]
        public async Task ChangeState(UserDto user, ViolationState state)
        {
            if (user != null) {
                await _formService.ChangeState(user.Id!, state);
            }
        }
    }
}

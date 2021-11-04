using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Server.Authentication;

namespace WhistleblowerSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttachementController : UserBaseController
    {
        private readonly AttachementService _attachementService;

        public AttachementController(UserManager userManager,
            IHttpContextAccessor httpContextAccessor,
            AttachementService attachementService) : base(httpContextAccessor, userManager)
        {
            _attachementService = attachementService;
        }

        [HttpPost]
        public async Task<AttachementMetaDataDto> Post([FromForm] IFormFile file)
        {
            await using MemoryStream stream = new();
            await file.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            byte[] data = stream.ToArray();
            
            return await _attachementService.SaveAttachementAsync(file.FileName,file.ContentType, data);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            //TODO prüfen ob Rechte File aufzumachen
            var attachementDto = await _attachementService.GetAttachementsByIdAsync(id);
            if (attachementDto == null) throw new Exception("File not found");
            return File(attachementDto.Bytes, attachementDto.Name.ToString());
        }
    }
}

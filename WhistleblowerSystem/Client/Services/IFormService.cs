using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public interface IFormService
    {
        public Task Load(FormDto formDto);
        public Task<FormDto> Save();
    }
}

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public interface IAttachementService
    {
        public Task<AttachementMetaDataDto?> Save(IBrowserFile file);
    }
}

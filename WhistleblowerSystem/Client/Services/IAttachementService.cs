using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public interface IAttachementService
    {
        public Task<AttachementMetaDataDto?> Save(IBrowserFile file);
    }
}

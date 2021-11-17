using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public interface IFormService
    {
        public Task Load(FormDto formDto);
        public Task<FormDto?> Save(FormDto _form);
        public Task<FormDto?> GetForm();

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.Models;

namespace WhistleblowerSystem.Client.Services
{
    public interface IFormService
    {
        public Task Load(FormDto formDto);
        public Task <List<FormDto>?>LoadAll();
        public Task<FormDto?> Save(FormDto _form);
        public Task<FormDto?> GetForm();
        public FormDto? GetCurrentForm();
        public void SetCurrentForm(FormDto? form);
        public FormModel? GetCurrentFormModel();
        public void SetCurrentFormModel(FormModel? form);
        public FormModel MapFormDtoToFormModel(FormDto dto);
    }
}

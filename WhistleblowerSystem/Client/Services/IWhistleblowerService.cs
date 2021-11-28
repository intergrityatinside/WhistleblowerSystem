using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public interface IWhistleblowerService
    {
        public Task <WhistleblowerDto?> Save(WhistleblowerDto whistleblower);

    }
}

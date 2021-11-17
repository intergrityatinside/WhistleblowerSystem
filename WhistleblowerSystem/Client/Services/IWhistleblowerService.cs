using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public interface IWhistleblowerService
    {
        public Task <WhistleblowerDto?> Save(WhistleblowerDto whistleblower);

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhistleblowerSystem.Shared.Provider;

namespace WhistleblowerSystem.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class BlockChainApiUriController : ControllerBase
    {
        BlockchainApiProvider _blockChainApiProvider;
        public BlockChainApiUriController(BlockchainApiProvider blockChainApiProvider)
        {
            _blockChainApiProvider = blockChainApiProvider;
        }

        [AllowAnonymous]
        [HttpGet]
        public string? Get() => _blockChainApiProvider.BaseUri;
    }
}

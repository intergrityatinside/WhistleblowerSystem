using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Business.Utils;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Repositories;
using WhistleblowerSystem.Shared.Exceptions;

namespace WhistleblowerSystem.Business.Services
{
    public class WhistleblowerService
    {
        readonly WhistleblowerRepository _whistleblowerRepository;
        readonly IMapper _mapper;

        public WhistleblowerService(WhistleblowerRepository whistleblowerRepository, IMapper mapper)
        {
            _whistleblowerRepository = whistleblowerRepository;
            _mapper = mapper;

        }

        public async Task<WhistleblowerDto> CreateWhistleblowerAsync(WhistleblowerDto whistleblowerDto)
        {
            Whistleblower whistleblower = _mapper.Map<Whistleblower>(whistleblowerDto);
            whistleblower.PasswordHash = PasswordUtils.HashPw(whistleblowerDto.Password);
            //todo validate => z.b. prüfen ob schon existiert
            if (whistleblowerDto.Password == null) throw new NullException(nameof(whistleblowerDto.Password));
            await _whistleblowerRepository.InsertOneAsync(whistleblower);
            return _mapper.Map<WhistleblowerDto>(whistleblower);
        }

        public async Task<bool> Authenticate(string id, string pw)
        {
            var user = await _whistleblowerRepository.GetByFormId(id);
            if (user == null || user.PasswordHash == null || !PasswordUtils.Verify(pw, user.PasswordHash))
            {
                return false;
            }

            return true;
        }

        public async Task<WhistleblowerDto?> FindOneByFormIdAsync(string id)
        {
            var whistleblower = await _whistleblowerRepository.GetByFormId(id);
            return _mapper.Map<WhistleblowerDto>(whistleblower);
        }
    }
}

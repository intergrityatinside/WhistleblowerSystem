using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Repositories;

namespace WhistleblowerSystem.Business.Services
{
    public class AttachementService
    {
        readonly AttachementRepository _attachementRepository;
        readonly IMapper _mapper;

        public AttachementService(AttachementRepository attachementRepository, IMapper mapper)
        {
            _attachementRepository = attachementRepository;
            _mapper = mapper;

        }

        public async Task<AttachementDto?> GetAttachementsByIdAsync(string id)
        {
            var attachement = await _attachementRepository.FindOneAsync(id);
            return _mapper.Map<AttachementDto>(attachement);
        }

        public async Task<AttachementMetaDataDto> SaveAttachementAsync(string filename, string filetype, byte[] bytes)
        {
            var contentType = new ContentType(filetype);
            Attachement attachement = new Attachement(null, contentType, bytes);
            await _attachementRepository.InsertOneAsync(attachement);
            return new AttachementMetaDataDto(filename, filetype, attachement.Id.ToString());
        }


    }
}

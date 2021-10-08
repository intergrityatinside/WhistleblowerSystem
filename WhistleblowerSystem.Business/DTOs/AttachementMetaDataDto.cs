using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.DTOs
{
    public class AttachementMetaDataDto
    {
        public AttachementMetaDataDto(string filename, string filetype, string attachementId)
        {
            Filename = filename;
            Filetype = filetype;
            AttachementId = attachementId;
        }

        public string Filename { get; set; }
        public string Filetype { get; set; }
        public string AttachementId { get; set; }
    }
}

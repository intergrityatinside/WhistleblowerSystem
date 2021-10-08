using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Database.Entities
{
    public class AttachementMetaData
    {
        public AttachementMetaData(string filename, string filetype, string attachementId)
        {
            Filename = filename;
            Filetype = filetype;

            if (!string.IsNullOrEmpty(attachementId))
            {
                AttachementId = ObjectId.Parse(attachementId);
            }
        }

        public string Filename { get; set; }
        public string Filetype { get; set; }
        public ObjectId AttachementId { get; set; }
    }
}

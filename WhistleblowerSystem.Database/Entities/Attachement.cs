using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Database.Entities
{
    public class Attachement
    {
        public Attachement(string? id, ContentType name, byte[] bytes)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Name = name;
            Bytes = bytes;
        }

        public ObjectId? Id { get; set; }
        public ContentType Name { get; set; }
        public Byte[] Bytes { get; set; }
    }
}

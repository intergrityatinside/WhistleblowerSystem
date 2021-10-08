using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.DTOs
{
    public class AttachementDto
    {
        public AttachementDto(string? id, ContentType name, byte[] bytes)
        {
            Id = id;
            Name = name;
            Bytes = bytes;
        }

        public string? Id { get; set; }
        public ContentType Name { get; set; }
        public Byte[] Bytes { get; set; }
    }
}

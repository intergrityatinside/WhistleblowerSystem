using System;
using System.Net.Mime;

namespace WhistleblowerSystem.Shared.DTOs
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

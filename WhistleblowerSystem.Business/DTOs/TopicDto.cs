using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.DTOs
{
    public class TopicDto
    {
        public TopicDto(string? id, string companyId, string name)
        {
            Id = id;
            CompanyId = companyId;
            Name = name;
        }

        public string? Id { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
    }
}

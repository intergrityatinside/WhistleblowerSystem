using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Database.Entities
{
    public class Topic
    {
        public Topic(string? id, string companyId, string name)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            if (!string.IsNullOrEmpty(companyId))
            {
                CompanyId = ObjectId.Parse(companyId);
            }
            Name = name;
        }

        public ObjectId? Id { get; set; }
        public ObjectId CompanyId { get; set; }
        public string Name { get; set; }
    }
}

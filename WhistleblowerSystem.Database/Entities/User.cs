using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;

namespace WhistleblowerSystem.Database.Entities
{
    public class User
    {
        public User(string? id, string companyId, string? passwordHash)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            if (!string.IsNullOrEmpty(companyId))
            {
                CompanyId = ObjectId.Parse(companyId);
            }
            PasswordHash = passwordHash;
        }

        [BsonId]
        public ObjectId? Id { get; set; }
        public ObjectId CompanyId { get; set; }
        public string? PasswordHash { get; set; }
    }
}

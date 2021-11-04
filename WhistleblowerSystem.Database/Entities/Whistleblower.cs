using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Database.Entities
{
     public class Whistleblower : IIdentifiable
    {
        public Whistleblower(string? id, string formId, string passwordHash)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            if (!string.IsNullOrEmpty(formId))
            {
                FormId = ObjectId.Parse(formId);
            }
            PasswordHash = passwordHash;
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId FormId { get; set; }
        public string PasswordHash { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Database.Entities
{
    public class FormMessage
    {
        public FormMessage(string? id, string text, User user, DateTime timestamp)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Text = text;
            User = user;
            Timestamp = timestamp;
        
        }
        [BsonId]
        public ObjectId? Id { get; set; }
        public string Text { get; set; }
        public User? User { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WhistleblowerSystem.Database.Entities
{
    public class User : IIdentifiable
    {
        public User(string? id, string passwordHash, string name, string firstName, string email)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            PasswordHash = passwordHash;
            Name = name;
            FirstName = firstName;
            Email = email;
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}

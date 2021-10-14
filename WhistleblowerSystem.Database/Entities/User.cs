using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WhistleblowerSystem.Database.Entities
{
    public class User : IIdentifiable
    {
        public User(string? id, string companyId, string passwordHash, string name, string firstName, string email)
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
            Name = name;
            FirstName = firstName;
            Email = email;
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId CompanyId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}

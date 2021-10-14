using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WhistleblowerSystem.Database.Entities
{
    public class Company : IIdentifiable
    {
        public Company(string? id, string name, string address, string zipcode)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Name = name;
            Address = address;
            Zipcode = zipcode;
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
    }
}

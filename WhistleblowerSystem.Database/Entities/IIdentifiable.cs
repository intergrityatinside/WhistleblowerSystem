using MongoDB.Bson;

namespace WhistleblowerSystem.Database.Entities
{
    public interface IIdentifiable
    {
        public ObjectId Id { get; set; }
    }
}

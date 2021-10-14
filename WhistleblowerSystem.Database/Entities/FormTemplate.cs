using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;


namespace WhistleblowerSystem.Database.Entities
{
    public class FormTemplate : IIdentifiable
    {
        public FormTemplate(string? id, List<FormFieldTemplate> fields)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Fields = fields;
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public List<FormFieldTemplate> Fields { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Database.Entities
{
    public class Form : IIdentifiable
    {
        public Form(string? id, string userId, string companyId, string topicId, string formTemplateId, List<FormField> formFields, List<AttachementMetaData>? attachements)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                UserId = ObjectId.Parse(userId);
            }
            if (!string.IsNullOrEmpty(companyId))
            {
                CompanyId = ObjectId.Parse(companyId);
            }
            if (!string.IsNullOrEmpty(topicId))
            {
                TopicId = ObjectId.Parse(topicId);
            }
            if (!string.IsNullOrEmpty(formTemplateId))
            {
                FormTemplateId = ObjectId.Parse(formTemplateId);
            }

            FormFields = formFields;
            Attachements = attachements;
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId CompanyId { get; set; }
        public ObjectId TopicId { get; set; }
        public ObjectId FormTemplateId { get; set; }
        public List<FormField> FormFields { get; set; }
        public List<AttachementMetaData>? Attachements { get; set; }
        public List<FormMessage>? Messages { get; set; }
        public ViolationState State { get; set; }

    }
}

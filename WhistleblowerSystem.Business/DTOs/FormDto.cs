using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.DTOs
{
    public class FormDto
    {
        public FormDto(string? id, string userId, string companyId, string topicId, string formTemplateId, List<FormFieldDto> formFields, List<AttachementMetaDataDto>? attachements)
        {
            Id = id;
            UserId = userId;
            CompanyId = companyId;
            TopicId = topicId;
            FormTemplateId = formTemplateId;
            FormFields = formFields;
            Attachements = attachements;
        }

        public string? Id { get; set; }
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public string TopicId { get; set; }
        public string FormTemplateId { get; set; }
        public List<FormFieldDto> FormFields { get; set; }
        public List<AttachementMetaDataDto>? Attachements { get; set; }

    }
}

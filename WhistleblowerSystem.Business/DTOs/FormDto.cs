using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Business.DTOs
{
    public class FormDto
    {
        public FormDto(string? id, string? topicId, string formTemplateId, List<FormFieldDto> formFields,
            List<AttachementMetaDataDto>? attachements, List<FormMessageDto>? messages, ViolationState state,
            string? pw)
        {
            Id = id;
            TopicId = topicId;
            FormTemplateId = formTemplateId;
            FormFields = formFields;
            Attachements = attachements;
            Messages = messages;
            State = state;
            Password = pw;
        }

        public string? Id { get; set; }
        public string? TopicId { get; set; }
        public string FormTemplateId { get; set; }
        public List<FormFieldDto> FormFields { get; set; }
        public List<AttachementMetaDataDto>? Attachements { get; set; }
        public List<FormMessageDto>? Messages { get; set; }
        public ViolationState State { get; set; }
        public string? Password { get; set; }
    }
}

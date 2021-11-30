using System;
using System.Collections.Generic;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Shared.Models
{
    public class FormModel
    {
        public FormModel(string? id, string? topicId, string formTemplateId, List<FormFieldDto> formFields,
            List<AttachementMetaDataDto>? attachements, List<FormMessageDto>? messages, ViolationState state,
            DateTime datetime, string? password, string title, string stateString)
        {
            Id = id;
            TopicId = topicId;
            FormTemplateId = formTemplateId;
            FormFields = formFields;
            Attachements = attachements;
            Messages = messages;
            State = state;
            Datetime = datetime;
            Password = password;
            Title = title;
            StateString = stateString;
        }
        
        public string? Id { get; set; }
            public string? TopicId { get; set; }
            public string FormTemplateId { get; set; }
            public List<FormFieldDto> FormFields { get; set; }
            public List<AttachementMetaDataDto>? Attachements { get; set; }
            public List<FormMessageDto>? Messages { get; set; }
            public ViolationState State { get; set; }
            public DateTime Datetime { get; set; }
            public string? Password { get; set; }
            public string Title { get; set; }
            public string StateString { get; set; }
        }
    }
   
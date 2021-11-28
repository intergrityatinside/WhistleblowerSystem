using System.Collections.Generic;

namespace WhistleblowerSystem.Shared.DTOs
{
    public class FormTemplateDto
    {
        public FormTemplateDto(string? id, List<FormFieldTemplateDto> fields)
        {
            Id = id;
            Fields = fields;
        }

        public string? Id { get; set; }
        public List<FormFieldTemplateDto> Fields { get; set; }
    }
}

using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.DTOs
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

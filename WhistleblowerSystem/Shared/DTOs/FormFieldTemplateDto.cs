using System.Collections.Generic;
using WhistleblowerSystem.Shared.DTOs.Config;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Shared.DTOs
{
    public class FormFieldTemplateDto
    {
        public FormFieldTemplateDto(string? id, List<LanguageEntryDto> text, ControlType type, List<SelectionValueDto>? selectionValues, bool mandatory)
        {
            Id = id;
            Text = text;
            Type = type;
            SelectionValues = selectionValues;
            Mandatory = mandatory;
        }

        public string? Id { get; set; }
        public List<LanguageEntryDto> Text { get; set; }
        public ControlType Type { get; set; }
        public List<SelectionValueDto>? SelectionValues { get; set; }
        public bool Mandatory { get; set; }

    }
}

using System.Collections.Generic;
using WhistleblowerSystem.Shared.DTOs.Config;
using WhistleblowerSystem.Shared.Enums;


namespace WhistleblowerSystem.Shared.DTOs
{
    public class FormFieldDto
    {
        public FormFieldDto(string? id, List<LanguageEntryDto> texts, ControlType type, List<string>? selectedValues, List<SelectionValueDto>? selectionValues, bool mandatory)
        {
            Id = id;
            Texts = texts;
            Type = type;
            SelectedValues = selectedValues!;
            SelectionValues = selectionValues;
            Mandatory = mandatory;
        }

        public string? Id { get; set; }
        public List<LanguageEntryDto> Texts { get; set; }
        public ControlType Type { get; set; }
        public List<string> SelectedValues { get; set; } = new List<string>(); // values which the user selected
        public List<SelectionValueDto>? SelectionValues { get; set; } // all values which can be selected
        public bool Mandatory { get; set; }
    }
}

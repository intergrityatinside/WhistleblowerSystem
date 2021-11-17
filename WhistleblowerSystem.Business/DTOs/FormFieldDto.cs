using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs.Config;
using WhistleblowerSystem.Database.Entities.Config;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Business.DTOs
{
    public class FormFieldDto
    {
        public FormFieldDto(string? id, List<LanguageEntryDto> texts, ControlType type, List<string>? selectedValues, List<SelectionValueDto>? selectionValues)
        {
            Id = id;
            Texts = texts;
            Type = type;
            SelectedValues = selectedValues;
            SelectionValues = selectionValues;
        }

        public string? Id { get; set; }
        public List<LanguageEntryDto> Texts { get; set; }
        public ControlType Type { get; set; }
        public List<string>? SelectedValues { get; set; } // values which the user selected
        public List<SelectionValueDto>? SelectionValues { get; set; } // all values which can be selected
    }
}

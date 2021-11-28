using System.Collections.Generic;
using WhistleblowerSystem.Shared.DTOs.Config;

namespace WhistleblowerSystem.Shared.DTOs
{
    public class SelectionValueDto
    {
        public SelectionValueDto(string? id, List<LanguageEntryDto> text, string value)
        {
            Id = id;
            Text = text;
            Value = value;
        }

        public string? Id { get; set; }
        public List<LanguageEntryDto> Text { get; set; }
        public string Value { get; set; }
    }
}

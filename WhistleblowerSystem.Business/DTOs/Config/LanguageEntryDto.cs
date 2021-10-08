using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Business.DTOs.Config
{
    public class LanguageEntryDto
    {
        public LanguageEntryDto(string? id , Language language, string text, string? value)
        {
            Id = id;
            Language = language;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Value = value;
            if (Language == Language.Undefined) throw new ArgumentException($"Property {nameof(LanguageEntryDto.Language)} must not be undefined.");
            if (string.IsNullOrEmpty(Text)) throw new ArgumentException($"Property {nameof(LanguageEntryDto.Text)} must not be empty.");
        }
        public string? Id { get; set; }
        public Language Language { get; set; }
        public string Text { get; set; }
        public string? Value { get; set; }
    }
}

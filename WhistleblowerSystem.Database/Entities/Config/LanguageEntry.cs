using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Database.Entities.Config
{
    public class LanguageEntry
    {
        public LanguageEntry(string? id, Language language, string text, string? value)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Language = language;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Value = value;
            if (Language == Language.Undefined) throw new ArgumentException($"Property {nameof(LanguageEntry.Language)} must not be undefined.");
            if (string.IsNullOrEmpty(Text)) throw new ArgumentException($"Property {nameof(LanguageEntry.Text)} must not be empty.");
        }
        public ObjectId? Id { get; set; }

        public Language Language { get; set; }
        public string Text { get; set; }
        public string? Value { get; set; }
    }
}

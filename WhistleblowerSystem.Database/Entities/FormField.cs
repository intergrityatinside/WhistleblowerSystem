using MongoDB.Bson;
using System.Collections.Generic;
using WhistleblowerSystem.Database.Entities.Config;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Database.Entities
{
    public class FormField
    {
        public FormField(string? id, List<LanguageEntry> texts, ControlType type, List<string> selectedValues, List<SelectionValue>? selectionValues)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Texts = texts;
            Type = type;
            SelectedValues = selectedValues;
            SelectionValues = selectionValues;
        }

        public ObjectId? Id { get; set; }
        public List<LanguageEntry> Texts { get; set; }
        public ControlType Type { get; set; }
        public List<string> SelectedValues { get; set; } // values which the user selected
        public List<SelectionValue>? SelectionValues { get; set; } // all values which can be selected
    }
}

using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities.Config;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Database.Entities
{
    public class FormField
    {
        public FormField(string? id, List<LanguageEntry> texts, ControlType type, List<SelectionValue> selectedValues, List<SelectionValue>? selectionValues)
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
        public List<SelectionValue> SelectedValues { get; set; } // values which the user selected
        public List<SelectionValue>? SelectionValues { get; set; } // all values which can be selected
    }
}

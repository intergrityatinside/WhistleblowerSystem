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
    public class FormFieldTemplate
    {
        public FormFieldTemplate(string? id, List<LanguageEntry> text, ControlType type, List<SelectionValue>? selectionValues, bool mandatory)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Text = text;
            Type = type;
            SelectionValues = selectionValues;
            Mandatory = mandatory;
        }

        public ObjectId? Id { get; set; }
        public List<LanguageEntry> Text { get; set; }
        public ControlType Type { get; set; }
        public List<SelectionValue>? SelectionValues { get; set; }
        public bool Mandatory { get; set; }


    }
}

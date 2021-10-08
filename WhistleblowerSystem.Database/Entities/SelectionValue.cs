using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities.Config;

namespace WhistleblowerSystem.Database.Entities
{
    public class SelectionValue
    {
        public SelectionValue(string? id, List<LanguageEntry> text, string value)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Text = text;
            Value = value;
        }

        public ObjectId? Id { get; set; }
        public List<LanguageEntry> Text { get; set; }
        public string Value { get; set; }
    }
}

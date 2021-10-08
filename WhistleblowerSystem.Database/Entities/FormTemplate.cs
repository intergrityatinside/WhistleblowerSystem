using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Database.Entities
{
    public class FormTemplate
    {
        public FormTemplate(string? id, List<FormFieldTemplate> fields)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Fields = fields;
        }

        public ObjectId? Id { get; set; }
        public List<FormFieldTemplate> Fields { get; set; }
    }
}

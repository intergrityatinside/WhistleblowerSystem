using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs.Config;

namespace WhistleblowerSystem.Business.DTOs
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

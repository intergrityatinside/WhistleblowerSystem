using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.DTOs
{
    public class WhistleblowerDto
    {
        public WhistleblowerDto(string? id, string formId, string password)
        {
            Id = id;
            FormId = formId;
            Password = password;
        }

        public string? Id { get; set; }
        public string FormId { get; set; }
        public string Password { get; set; }
    }
}

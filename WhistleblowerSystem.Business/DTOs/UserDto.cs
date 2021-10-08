using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.DTOs
{
    public class UserDto
    {
        public UserDto(string? id, string companyId, string? password)
        {
            Id = id;
            CompanyId = companyId;
            if (password != null) {
                Password = password;
            }
        }

        public string? Id { get; set; }
        public string CompanyId { get; set; }
        public string? Password { get; set; }
    }
}

using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.DTOs
{
    public class CompanyDto
    {
        public CompanyDto(string? id, string name, string address, string zipcode)
        {
            Id = id;
            Name = name;
            Address = address;
            Zipcode = zipcode;
        }

        public string? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
    }
}

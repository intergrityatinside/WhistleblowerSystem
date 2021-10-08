using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Database.Entities
{
    public class Company
    {
        public Company(string? id, string name, string address, string zipcode)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
            Name = name;
            Address = address;
            Zipcode = zipcode;
        }

        public ObjectId? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
    }
}

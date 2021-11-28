namespace WhistleblowerSystem.Shared.DTOs
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

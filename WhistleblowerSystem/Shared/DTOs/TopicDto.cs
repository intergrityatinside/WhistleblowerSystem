namespace WhistleblowerSystem.Shared.DTOs
{
    public class TopicDto
    {
        public TopicDto(string? id, string companyId, string name)
        {
            Id = id;
            CompanyId = companyId;
            Name = name;
        }

        public string? Id { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
    }
}

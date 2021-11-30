namespace WhistleblowerSystem.Shared.DTOs
{
    public class UserDto
    {
        public UserDto(string? id, string password, string name, string firstName, string email)
        {
            Id = id;
            Password = password;
            Name = name;
            FirstName = firstName;
            Email = email;
        }

        public string? Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

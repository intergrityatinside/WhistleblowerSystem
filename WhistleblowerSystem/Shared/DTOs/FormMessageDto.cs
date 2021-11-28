using System;

namespace WhistleblowerSystem.Shared.DTOs
{
    public class FormMessageDto
    {
        public FormMessageDto(string? id, string text, UserDto user, DateTime timestamp)
        {
            Id = id;
            Text = text;
            User = user;
            Timestamp = timestamp;
        }

        public string? Id { get; set; }
        public string Text { get; set; }
        public UserDto? User { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

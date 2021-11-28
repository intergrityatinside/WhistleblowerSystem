namespace WhistleblowerSystem.Shared.DTOs
{
    public class AttachementMetaDataDto
    {
        public AttachementMetaDataDto(string filename, string filetype, string attachementId)
        {
            Filename = filename;
            Filetype = filetype;
            AttachementId = attachementId;
        }

        public string Filename { get; set; }
        public string Filetype { get; set; }
        public string AttachementId { get; set; }
    }
}

using Microsoft.AspNetCore.Http;

namespace EmailSender
{
    public class EmailMessageModel
    {
        public string From { get; set; } = null!;

        public List<string> To { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Content { get; set; } = null!;

        public List<IFormFile> Attachments { get; set; } = null!;
    }
}

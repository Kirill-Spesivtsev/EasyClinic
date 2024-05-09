using Microsoft.AspNetCore.Http;

namespace KEmailSender
{
    /// <summary>
    /// Model, representing an email message.
    /// </summary>
    public class EmailMessageModel
    {
        /// <summary>
        /// Sender's email address.
        /// </summary>
        public string From { get; set; } = null!;

        /// <summary>
        /// Email addresses of recipients.
        /// </summary>
        public List<string> To { get; set; } = null!;

        /// <summary>
        /// Email message title.
        /// </summary>
        public string Subject { get; set; } = null!;

        /// <summary>
        /// Email message content.
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// Email attachments if any.
        /// </summary>
        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
    }
}

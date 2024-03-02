using MailKit;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailMessageModel messageDto)
        {
            using var message = new MimeMessage();
            using var client = new SmtpClient();

            foreach (string address in messageDto.To)
            {
                message.To.Add(MailboxAddress.Parse(address));
            }
            message.From.Add(MailboxAddress.Parse(messageDto.From));
            message.Subject = messageDto.Subject;
            
            var builder = new BodyBuilder();
            builder.HtmlBody = messageDto.Content;
            if (messageDto.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var file in messageDto.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            message.Body = builder.ToMessageBody();

            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(_configuration["EmailSender:Username"], _configuration["EmailSender:Password"]);
            await client.SendAsync(message);
            client.Disconnect(true);
        }
    }
}

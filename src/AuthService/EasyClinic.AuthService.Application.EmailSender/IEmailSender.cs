namespace EmailSender
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(EmailMessageModel messageDto);
    }
}

namespace EmailSender
{
    /// <summary>
    /// Sends emails.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends email corresponding to the given message model.
        /// </summary>
        /// <param name="messageDto"></param>
        /// <returns></returns>
        public Task SendEmailAsync(EmailMessageModel messageDto);
    }
}

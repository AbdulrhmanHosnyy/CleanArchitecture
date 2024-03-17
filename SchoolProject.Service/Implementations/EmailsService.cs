﻿using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class EmailsService : IEmailsService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion

        #region Constructors
        public EmailsService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion

        #region Functions
        public async Task<string> SendEmail(string email, string msg)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{msg}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Future Team", _emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("testing", email));
                    //message.Subject = reason == null ? "No Submitted" : reason;
                    message.Subject = "New Contact Submitted Data";
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
        #endregion

    }
}
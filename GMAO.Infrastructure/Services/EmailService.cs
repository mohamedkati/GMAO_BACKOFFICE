using GMAO.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using GMAO.Application.Common.AppSettings;
using GMAO.Application.Common.Exceptions;

namespace GMAO.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSettings;

        public EmailService(IOptions<EmailSetting> emailSettings)
        {
            this._emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {

                using (var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_emailSettings.User, _emailSettings.Password);
                    var mailMessage = new MailMessage()
                    {
                        From = new MailAddress(_emailSettings.From),
                        IsBodyHtml = true,
                        Body = body,
                        Subject = subject
                    };

                    mailMessage.To.Add(to);
                    await client.SendMailAsync(mailMessage);
                }

            }
            catch (System.Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

    }
}

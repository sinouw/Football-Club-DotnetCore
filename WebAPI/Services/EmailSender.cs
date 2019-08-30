using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;



        public EmailSender(IConfiguration config)
        {
            _config = config;

        }


        public void Send(string SendTo, string body, string subject)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            try
            {
                var emailSettings = new EmailSettings();
                _config.GetSection("EmailSetting").Bind(emailSettings);
                client.Credentials = new NetworkCredential(emailSettings.EmailAdress, emailSettings.Pwd);
                client.EnableSsl = true;
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(emailSettings.EmailAdress);
                mailMessage.To.Add(SendTo);
                mailMessage.Body = body;
                mailMessage.Subject = subject;
                try
                {
                    client.Send(mailMessage);


                }
                catch (Exception e)
                {

                    throw;
                }

            }
            catch (Exception e)
            {

                throw;
            }
            /* try
            {
                var x = _configuration.GetSection("EmailSetting");
                var y = _configuration.GetSection("EmailSetting.pwd");
            }
            catch (Exception e)
            {

                throw;
            } */

        }


    }
}
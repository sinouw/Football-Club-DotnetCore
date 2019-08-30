using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using System.Net.Mail;

namespace WebAPI.Controllers.mailingUSer
{
    [Route("api/Mailing")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public MailingController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        // POST: api/Addresses


        [HttpPost("{email}")]
        public ActionResult SendMail(string email,string body)
        {
            string subject = "Sports Ball Reservation";
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            _emailSender.Send(email, body, subject);
            return Ok();
        }
    }
}
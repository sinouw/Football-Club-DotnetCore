using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
  public  interface IEmailSender
    {
        void Send(string SendTo, string body, string subject);
    }
}

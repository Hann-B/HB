using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string message);
    }
}

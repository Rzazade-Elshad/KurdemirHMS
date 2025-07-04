using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ExternalServices.Abstractions
{
    public interface IEmailService
    {
        public void SendEmailConfirmation(string reciever, string name, int token);
    }
}

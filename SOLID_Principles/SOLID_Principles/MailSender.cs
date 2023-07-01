using System;
using System.Collections.Generic;
using System.Text;

namespace SRP
{
    public class MailSender
    {
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }

        public void SendEmail()
        {
            //Here is the Logic to send Email
        }
    }
}

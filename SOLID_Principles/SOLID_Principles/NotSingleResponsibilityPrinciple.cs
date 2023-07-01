using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SOLID_Principles
{
    //Here this class is not following SRP, This class is for Invoice purpose but its dealing with Email sending and Logging as well
    public class NotSingleResponsibilityPrinciple
    {
        public long InvoiceAmount { get; set; }
        public DateTime InvoiceDate { get; set; }

        public void AddInvoice()
        {
            //Here is the logic for adding Invoice
            MailMessage mailMessage = new MailMessage("EmailFrom", "Email To", "EmailSubject", "EmailBody");
            this.SendEmail(mailMessage);
        }

        public void DeleteInvoice()
        {
            try
            {
                //Here is the logic for deleting Invoice
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("C:/ErrotText.txt", ex.ToString());
            }
        }

        private void SendEmail(MailMessage mailMessage)
        {
            try
            {
                //Logic to send an email
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("C:/ErrotText.txt", ex.ToString());
            }
        }
    }
}

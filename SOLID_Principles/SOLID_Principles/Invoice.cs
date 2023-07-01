using SRP;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SOLID_Principles
{
    public class Invoice
    {
        public long InvoiceAmount { get; set; }
        public DateTime InvoiceDate { get; set; }

        public ILogger logger;
        public MailSender mailSender;

        public Invoice(ILogger logger, MailSender mailSender)
        {
            this.logger = logger;
            this.mailSender = mailSender;
        }

        public void AddInvoice()
        {
            //here is teh logic to add Invoice
            mailSender.EmailFrom = "Email From";
            mailSender.EmailTo = "Email To";
            mailSender.EmailSubject = "Email Subject";
            mailSender.EmailBody = "Emai Body";
            mailSender.SendEmail();
            logger.Info("Message to be logged");
        }

        public void DeleteInvoice()
        {
            try
            {
                //Here is ;the logic to  delete invoice
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
            }
        }
    }
}

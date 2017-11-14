using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspection_mvc.Models.EF;
using System.Threading.Tasks;
using System.Net.Mail; 

namespace Inspection_mvc.Helpers
{
    public class EmailService
    {
        private SmtpClient client;
        private int port = 25;

        public bool errorFlag = false;
        public string errorMessage = "";

        public EmailService(SmtpClient _client = null, int _port = 25)
        {
            client = (_client == null) ? new SmtpClient("10.100.10.97") : client;
            port = _port; 
        }
        public void sendAsync(Email tosend)
        {
            if (tosend.toaddress.Trim().Length == 0)
            {
                errorFlag = true;
                errorMessage = "email must contain a destination address."; 
            }

            if (tosend.fromaddress.Trim().Length == 0)
            {
                errorFlag = true;
                errorMessage = "email must contain from address.";
            }

            if (tosend.body.Trim().Length == 0)
            {
                errorFlag = true;
                errorMessage = "email must contain body"; 
            }
            send(tosend);
            //Task.Run(() => send(tosend)); 
        }

        private void send(Email tosend)
        {
            MailMessage message = new MailMessage(tosend.fromaddress, tosend.toaddress);

            if (tosend.bcc != null)
            {
                foreach (var item in tosend.bcc)
                    message.Bcc.Add(item);
            }

            if (tosend.cc != null)
            {
                foreach (var item in tosend.cc)
                    message.CC.Add(item);
            }     

            message.IsBodyHtml = tosend.isBodyHtml;
            message.Subject = tosend.subject;
            message.Body = tosend.body; 

            client.Port = port;
            client.UseDefaultCredentials = false;
            client.Send(message); 
        }

        public string[] GetRollUsers()
        {
            Models.EF.AprManager context = new Models.EF.AprManager(); 

            if (context != null)
            {
                try
                {
                    return (from x in context.EmailMasters where x.ROLLINS_ALERT_EMAIL == true select x.Address).ToArray();
                } catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
                }
            }
            return new string[]{ };
        }
    }
    public class Email
    {
        public string toaddress { get; set; }
        public string fromaddress { get; set; }
        public string[] cc { get; set; }
        public string[] bcc { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool isBodyHtml { get; set; }
    }
}
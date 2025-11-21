using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Datos
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;
        public EmailService() 
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("8ffdcb68afcb32", "db54e243e0b6f3");
            server.EnableSsl = true;
            server.Port = 2525; 
            server.Host = "sandbox.smtp.mailtrap.io";
        }

        public void armarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@ejemplotpnivel3.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            //email.Body = "";
            email.Body = cuerpo;
        }
        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

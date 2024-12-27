using Ekidait.Core.Entities;
using System.Net;
using System.Net.Mail;
namespace Ekidait.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact)
        {
            SmtpClient  smtpClient = new SmtpClient("mail.siteadi.com",587);
            smtpClient.Credentials = new NetworkCredential("Dilaratqz@gmail.com", "mailşifresi");
            smtpClient.EnableSsl = false;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("Dilaratqz@gmail.com");
            message.To.Add("Dilaratqz@gmail.com");
            message.Subject = "Siteden Mesaj Geldi";
            message.Body = $"İsim:{contact.Name} - Soyadı:{contact.Surname} - Email:{contact.Email} - Telefon:{contact.Phone} - Mesaj:{contact.Message}";
            message.IsBodyHtml = true;
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}

using System.Net.Mail;

namespace codeKade.Application.Senders
{
    public class EmailSender
    {
        public static bool SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.mail.yahoo.com");
            mail.From = new MailAddress("yoonesasgarian@yahoo.com", "کد کده");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            
            SmtpServer.Port = 465;
            SmtpServer.EnableSsl = true;

            SmtpServer.Credentials = new System.Net.NetworkCredential("yoonesasgarian@yahoo.com", "yoo1383nes");
            SmtpServer.Send(mail);
            return true;
        }
    }
}

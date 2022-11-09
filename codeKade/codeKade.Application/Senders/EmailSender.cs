using System.Net.Mail;

namespace codeKade.Application.Senders
{
    public class EmailSender
    {
        public static bool SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("yoones1383asgarian@gmail.com", "تاپ لرن");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;

            SmtpServer.Credentials = new System.Net.NetworkCredential("yoones1383asgarian@gmail.com", "Yoones1382Asgarian");
            SmtpServer.Send(mail);
            return true;
        }
    }
}

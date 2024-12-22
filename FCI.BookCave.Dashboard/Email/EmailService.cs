using System.Net.Mail;
using System.Net;

namespace FCI.BookCave.Dashboard.Email
{
    public class EmailService : IEmailService
    {
        public Task sendAsync(string email, string subject, string content)
        {
            string mail = "gwork1233@outlook.com";
            string pass = "Password123##";

            //var client = new SmtpClient("smtp.office365.com", 587)
            //{
            //    EnableSsl = true,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(mail, pass)
            //};
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(mail, pass);
            return smtp.SendMailAsync(
            new MailMessage(from: "gwork1233@outlook.com",
                            to: email,
                            subject,
                            content
                            ));
        }
    }
}

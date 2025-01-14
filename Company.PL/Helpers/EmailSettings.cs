using Company.DAL.Model;
using System.Net;
using System.Net.Mail;

namespace Company.PL.MappingProfilesss
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com" , 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ahme6afathi@gmail.com" , "jxzftupdggowbbil");
            client.Send("ahme6afathi@gmail.com", email.Recipients, email.Subject, email.Body);
        }
    }
}

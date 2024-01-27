using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Email
{
    public interface IUserMailer
    {
        Task SendHtmlMailWithAttachment(string mailTo, string subject, string body, string filename, string contentType, MemoryStream stream);
        void SendMail(string mailTo, string subject, string body);
        void SendMailMultiple(List<string> mailTo, string subject, string body);
        void SendMailMultipleWithInMemoryAttachment(List<string> mailTo, string subject, string body, string filename, string contentType, MemoryStream stream);
        Task SendMailHtmlMultiple(List<string> mailTo, string subject, string body);
        void SendHtmlMail(string mailTo, string subject, string body);
        Task SendHtmlMailWithAttachment(string mailTo, string subject, string body, string attachment_file);
        Task SendMailHtmlMultipleWithCC(string mailTo, List<string> cc, string subject, string body);
    }
}

using Movie.Core.Messaging.Email.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Email
{
    public interface IMailer
    {
        void SendMail(string mailTo, string subject, string body);
        void SendMailMultiple(List<string> mailTo, string subject, string body);
        void SendMailMultipleWithInMemoryAttachment(List<string> mailTo, string subject, string body, string filename, string contentType, MemoryStream stream);
        Task SendMailHtmlMultiple(List<string> mailTo, string subject, string body);
        void SendHtmlMail(string mailTo, string subject, string body);
        Task SendHtmlMailWithAttachment(string mailTo, string subject, string body, string attachment_file);
        Task SendMailHtmlMultipleWithCC(string mailTo, List<string> cc, string subject, string body);
    }

    public class Mailer : IMailer
    {

        private readonly MailMessage _mail;
        private readonly SmtpClient _smtpServer;
        private readonly ILogger<Mailer> _logger;

        public Mailer(EmailConfiguration emailConfig, ILogger<Mailer> logger)
        {
            _logger = logger;
            var credentials = emailConfig;

            MailMessage mail = new MailMessage();
            
            SmtpClient smtpServer = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(credentials.UserName, credentials.Password),
                Port = credentials.Port,
                Host = credentials.Host,
                EnableSsl = credentials.UseSsl,
            };

            _smtpServer = smtpServer;
            mail.From = new MailAddress(credentials.UserName,
                "GIGM.com", Encoding.UTF8);
            _mail = mail;
        }

        public void SendMail(string mailTo, string subject, string body)
        {
            _mail.To.Clear();
            _mail.To.Add(mailTo);
            _mail.Subject = subject;
            _mail.Body = body;

            _smtpServer.Send(_mail);

        }

        public void SendMailMultiple(List<string> mailTo, string subject, string body)
        {
            _mail.To.Clear();

            if (mailTo != null)
            {
                foreach (var mail in mailTo)
                {
                    // _mail.To.Clear();
                    _mail.To.Add(mail);
                }

                _mail.Subject = subject;
                _mail.Body = body;

                _smtpServer.Send(_mail);
            }


        }

        public void SendMailMultipleWithInMemoryAttachment(List<string> mailTo, string subject, string body, string filename, string contentType, MemoryStream stream)
        {
            _mail.To.Clear();

            if (mailTo != null)
            {
                foreach (var mail in mailTo)
                {

                    _mail.To.Add(mail);
                }

                _mail.Subject = subject;
                _mail.Body = body;
                if (stream != null)
                {
                    Attachment attachment = new Attachment(stream, filename, contentType);
                    _mail.Attachments.Add(attachment);

                }

                _smtpServer.Send(_mail);
            }

        }

        public Task SendMailHtmlMultipleWithCC(string mailTo, List<string> cc, string subject, string body)
        {
            _mail.To.Clear();

            if (mailTo != null)
            {
                _mail.To.Add(mailTo);

                foreach (var copy in cc)
                {
                    _mail.CC.Add(copy);
                }

                _mail.Subject = subject;
                _mail.Body = body;
                _mail.IsBodyHtml = true;

                try
                {
                    _smtpServer.Send(_mail);
                }
                catch
                {

                }
            }
            return Task.FromResult(true);

        }

        public Task SendMailHtmlMultiple(List<string> mailTo, string subject, string body)
        {
            _mail.To.Clear();

            if (mailTo != null)
            {
                foreach (var mail in mailTo)
                {
                    // _mail.To.Clear();
                    _mail.To.Add(mail);
                }

                _mail.Subject = subject;
                _mail.Body = body;
                _mail.IsBodyHtml = true;

                _smtpServer.Send(_mail);
            }
            return Task.FromResult(true);

        }
        public void SendHtmlMail(string mailTo, string subject, string body)
        {
            
            _mail.To.Clear();
            _mail.To.Add(mailTo);
            _mail.Subject = subject;
            _mail.Body = body;
            _mail.IsBodyHtml = true;

            _smtpServer.Send(_mail);

        }

        public Task SendHtmlMailWithAttachment(string mailTo, string subject, string body, string attachment_file)
        {
            _mail.To.Clear();
            _mail.To.Add(mailTo);
            _mail.Subject = subject;
            _mail.Body = body;
            _mail.IsBodyHtml = true;


            if (attachment_file != null)
            {
                Attachment attachment = new Attachment(attachment_file, MediaTypeNames.Application.Octet);
                ContentDisposition? disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(attachment_file);
                disposition.ModificationDate = File.GetLastWriteTime(attachment_file);
                disposition.ReadDate = File.GetLastAccessTime(attachment_file);
                disposition.FileName = Path.GetFileName(attachment_file);
                disposition.Size = new FileInfo(attachment_file).Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                _mail.Attachments.Add(attachment);
            }

            _smtpServer.Send(_mail);

            return Task.FromResult(true);
        }

    }
}

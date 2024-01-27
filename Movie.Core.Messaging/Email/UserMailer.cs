//using Common.Logging;
using Movie.Core.Messaging.Config;
using Movie.Core.Messaging.Config.Elements;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Email
{
    public class UserMailer : IUserMailer
    {

        private readonly MailMessage _mail;
        private readonly SmtpClient _smtpServer;
        // protected ILog Logger;
        public IConfiguration Configuration { get; }
        public UserMailer(IConfiguration configuration, SmtpNode smtpNode)
        {
            Configuration = configuration;
            // Logger = logger;

            //var credentials = SmtpConfig.Get().SmtpNode;
            //var credentials = Configuration.GetSection("smtpNode").;

            MailMessage mail = new MailMessage();



            SmtpClient smtpServer = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(smtpNode.UserName, smtpNode.Password),
                Port = smtpNode.Port,
                Host = smtpNode.Host,
                EnableSsl = smtpNode.UseSsl,
                //UseDefaultCredentials = false

            };
            _smtpServer = smtpServer;
            mail.From = new MailAddress(smtpNode.UserName,
                "GIGM.com", System.Text.Encoding.UTF8);
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
            //try
            //{
            //    Logger.Info("mail to:" + mailTo + "//" + DateTime.Now.ToString());
            //}
            //catch
            //{

            //}
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
                ContentDisposition disposition = attachment.ContentDisposition;
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


        public Task SendHtmlMailWithAttachment(string mailTo, string subject, string body, string filename, string contentType, MemoryStream stream)
        {
            _mail.To.Clear();
            _mail.To.Add(mailTo);
            _mail.Subject = subject;
            _mail.Body = body;
            _mail.IsBodyHtml = true;

            if (stream != null)
            {
                Attachment attachment = new Attachment(stream, filename, contentType);
                _mail.Attachments.Add(attachment);
            }

            _smtpServer.Send(_mail);
            return Task.FromResult(true);
        }
    }
}

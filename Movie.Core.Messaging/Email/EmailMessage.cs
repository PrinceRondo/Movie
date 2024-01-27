using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Email
{
  public class EmailMessage
  {
    public MailboxAddress To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public EmailMessage(string to, string content, string subject)
    {

      To = new MailboxAddress(to,to);
      Subject = subject;
      Content = content;
    }
  }
}

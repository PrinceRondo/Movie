using MimeKit;
using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using Movie.Core.Messaging.Email;
using Movie.Core.Messaging.Email.Model;

namespace Movie.Core.Messaging.Email
{
  public class EmailRepo : IEmailRepo
  {
    private readonly EmailConfiguration _emailConfig;

    public EmailRepo(EmailConfiguration emailConfig)
    {
      _emailConfig = emailConfig;
    }
    public void SendEmail(EmailMessage message)
    {
      var emailMessage = CreateEmailMessage(message);
      Send(emailMessage);
    }
    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
      try
      {
         BodyBuilder bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = "<h2>HORMS</h2>";
        bodyBuilder.TextBody = message.Content;

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
        emailMessage.To.Add(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

        return emailMessage;
      }
      catch (Exception)
      {
        throw;
      }
    }

    private void Send(MimeMessage mailMessage)
    {
      using var client = new SmtpClient();
      try
      {
        client.Connect(_emailConfig.Host, _emailConfig.Port, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
        client.Send(mailMessage);
      }
      catch(Exception ex)
      {
        //log an error message or throw an exception or both.
        throw new Exception(ex.Message);
      }
      finally
      {
        client.Disconnect(true);
        client.Dispose();
      }
    }
  }
}

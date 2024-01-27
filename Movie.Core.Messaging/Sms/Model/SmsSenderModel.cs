using GIGMS.Core.Messaging.Sms;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Movie.Core.Messaging.Sms.Model
{
    public class SmsSenderModel : SmsSender
    {
        //private static readonly IWebClient WebClient = SmsNotification.WebClientSource;
        public string ApiUrl { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public IWebClient WebClient => SmsNotification.WebClientSource;



        protected ILog Logger;

        public override Task SendSmsAsync() => Task.Factory.StartNew(() =>
        {
            WebClient.DoRequest(ApiUrl, Method, Body);
        });

        public override void SendSms()
        {
            WebClient.DoRequest(ApiUrl, Method, Body);
        }

        public override Task SendTwilioSmsAync(string toNumber, string body) => Task.Factory.StartNew(() =>
        {

            Logger = LogManager.GetLogger("Sms----");

            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            var authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            var fromPhoneNumber = ConfigurationManager.AppSettings["TwilioSender"];


            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(fromPhoneNumber),
                body: body);

            Console.WriteLine(message.Sid);
            Logger.Info(message.Sid);
        });


       }

}

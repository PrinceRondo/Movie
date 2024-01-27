using Movie.Core.Common.Enums;
using Movie.Core.Messaging.Common.Helpers;
using Movie.Core.Messaging.Sms;
using Movie.Core.Messaging.Sms.Model;
using System.Configuration;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Movie.Core.Messaging.Sms
{
    public class SmsNotification : ISmsNotification
    {
        public static IWebClient WebClientSource;

        public SmsNotification( IWebClient webClient)
        {
            WebClientSource = webClient;
            // _logger = logger;
        }
        public void SendSmsNowAsync(string message, string[] recipient, string sender = "", string commandType = "sendquickmsg")
        {
            var model = GetSmartSmsModel(message, recipient, sender, commandType);
            SmartSms(model).SendSmsAsync();
            
        }

        public void SendSmsNowBankModeAsync(string message, string[] recipient, string sender = "", string commandType = "sendquickmsg")
        {
            var model = GetSmartSmsModel(message, recipient, sender, commandType);
            SmartSmsBankMode(model).SendSmsAsync();

        }

        SmsLiveModel GetSmartSmsModel(string message, string[] recipient, string sender = "", string commandType = "sendquickmsg")
        {
            var smsDetails = new SmsLiveModel
            {
                CommandType = commandType,
                Message = message,
                Sender = sender,
                Recipient = recipient,
                SmsType = (int)SmsType.Normal,
                Token = ConfigurationManager.AppSettings["SmartSmsToken"],

            };
            return smsDetails;
        }

        public SmsSenderModel SmartSms(SmsLiveModel model)
        {
            var smsBody = model.Message;
            var recipient = model.Recipient.ArrayToCommaSeparatedString().UrlEncode();

            //var url = "https://smartsmssolutions.com/api/?sender=GIGM.COM&to=" + recipient + "&message=" + smsBody + "&type=0&routing=3&token=" + token;
            // var url= "http://www.ogosms.com/dynamicapi/?username=gigm&password=gigasmin&sender=GIGM.COM&route=1&numbers="+recipient+"&message="+smsBody;

            var url = "http://www.ogosms.com/dynamicapi?username=gigm&password=gigasmin&sender=GIGM.COM&route=bank&numbers=" + recipient + "&message=" + smsBody;

            //var url = "http://www.ogosmsserver.com/dynamicapi?username=gigm&password=gigasmin&sender=GIGM.COM&route=1&numbers=" + recipient + "&message=" + smsBody;

            var senderModel = new SmsSenderModel
            {
                ApiUrl = url,
                Method = "GET"

            };
            return senderModel;

        }

        public SmsSenderModel SmartSmsBankMode(SmsLiveModel model)
        {
            var smsBody = model.Message;
            var recipient = model.Recipient.ArrayToCommaSeparatedString().UrlEncode();

            //var url = "https://smartsmssolutions.com/api/?sender=GIGM.COM&to=" + recipient + "&message=" + smsBody + "&type=0&routing=3&token=" + token;
            // var url= "http://www.ogosms.com/dynamicapi/?username=gigm&password=gigasmin&sender=GIGM.COM&route=1&numbers="+recipient+"&message="+smsBody;

            //var url = "http://www.ogosms.com/dynamicapi?username=gigm&password=gigasmin&sender=GIGM.COM&route=bank&numbers=" + recipient + "&message=" + smsBody;

            var url = "http://www.ogosmsserver.com/dynamicapi?username=gigm&password=My1Tadmin&sender=GIGM.COM&route=bank&numbers=" + recipient + "&message=" + smsBody;

            var senderModel = new SmsSenderModel
            {
                ApiUrl = url,
                Method = "GET"

            };
            return senderModel;

        }

        public void SendTwilioSmsAsync(string toNumber, string body)
        {

            var twilioRestClient = ProxiedTwilioClientCreator.GetClient();

            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            var authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            var fromPhoneNumber = ConfigurationManager.AppSettings["TwilioSender"];

            TwilioClient.Init(accountSid, authToken);


            // Now that we have our custom built TwilioRestClient,
            // we can pass it to any REST API resource action.

            Task.Factory.StartNew(() => {
                var message = MessageResource.Create(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(fromPhoneNumber),
                body: body);

                // Console.WriteLine("SIS" + message.Sid);
            });
        }

        public void SendTwilioSms(string toNumber, string body)
        {
            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            var authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            var fromPhoneNumber = ConfigurationManager.AppSettings["TwilioSender"];


            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(fromPhoneNumber),
                body: body);

        }

        public void SendTwilioSmsNowAsync(string toNumber, string body)
        {
            SendTwilioSmsAsync(toNumber, body);

            SmartSms(null).SendTwilioSmsAync(toNumber, body);
        }
    }
}
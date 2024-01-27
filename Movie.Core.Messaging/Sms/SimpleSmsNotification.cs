using Movie.Core.Common.Enums;
using Movie.Core.Messaging.Sms.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Sms
{
    public class SimpleSmsNotification : ISmsNotification
    {
        public static IWebClient _webClientSource;
        private readonly SmsConfiguration _smsConfig;
        private readonly string _apiKey;

        public SimpleSmsNotification(IWebClient webClient, SmsConfiguration smsConfig)
        {
            _webClientSource = webClient;
            _smsConfig = smsConfig;
            _apiKey = ConfigurationManager.AppSettings["SmsApi"] ?? "http://www.ogosms.com/dynamicapi/";
        }
        public void SendSmsNowAsync(string message, string[] recipient, string sender = "", string commandType = "sendquickmsg")
        {
            var model = GetSmartSmsModel(message, recipient, sender, commandType);
            SmartSms(model).SendSmsAsync();
            //}
        }

        public void SendSmsNowBankModeAsync(string message, string[] recipient, string sender = "", string commandType = "sendquickmsg")
        {
            throw new NotImplementedException();
        }

        public void SendTwilioSms(string toNumber, string body)
        {
            throw new NotImplementedException();
        }

        public void SendTwilioSmsNowAsync(string toNumber, string body)
        {
            throw new NotImplementedException();
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
                Token = _smsConfig.SmsSmartToken,

            };
            return smsDetails;
        }


        public SmsSenderModel SmartSms(SmsLiveModel model)
        {

            //http://www.ogosms.com/smsapi/?username=gigm&password=gigasmin&sender=GIGM.COM&numbers=09087821914&message=Welcome


            var smsBody = model.Message;
            var recipient = model.Recipient.ArrayToCommaSeparatedString().UrlEncode();
            var token = model.Token;
            //    var url = SmsKeys.GtsSmsUrl + "?"+ smsBody;
            var url = $"{_smsConfig.SmsApi}?username=gigm&password=gigasmin&sender=GIGM.COM&numbers=" + recipient + "&message=" + smsBody;
            //var url = "https://smartsmssolutions.com/api/?sender=GIGM.COM&to=" + recipient + "&message=" + smsBody + "&type=0&routing=3&token=" + token;
            var senderModel = new SmsSenderModel
            {
                ApiUrl = url,
                Method = "GET"
            };

            return senderModel;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Sms
{
    public interface ISmsNotification
    {
        void SendSmsNowAsync(string message, string[] recipient, string sender = "", string commandType = "sendquickmsg");
        void SendSmsNowBankModeAsync(string message, string[] recipient, string sender = "", string commandType = "sendquickmsg");

        void SendTwilioSmsNowAsync(string toNumber, string body);

        void SendTwilioSms(string toNumber, string body);


    }
}

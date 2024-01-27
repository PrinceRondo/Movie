using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIGMS.Core.Messaging.Sms
{
    public abstract class SmsSender
    {
        public abstract Task SendSmsAsync();
        public abstract void SendSms();
        public abstract Task SendTwilioSmsAync(string toNumber, string body);
    }
}

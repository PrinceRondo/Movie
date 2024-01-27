using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Sms.Model
{
   public abstract class SmsBase
    {
        public abstract string ToEncodedUrl(object content);
        public abstract string ToEncodedUrl2();
    }
}

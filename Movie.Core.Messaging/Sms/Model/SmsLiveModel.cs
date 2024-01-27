﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Sms.Model
{
    public class SmsLiveModel 
    {
        public string CommandType { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public string Token { get; set; }
        public string[] Recipient { get; set; }
        public int SmsType { get; set; }

    }
}
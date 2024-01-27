using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Sms.Model
{
  public class TwilioConfiguration
  {
    public string TwilioAccountSid { get; set; }
    public string TwilioAuthToken { get; set; }
    public string TwilioSender { get; set; }
    public DateTime NoTripsDay { get; set; }
    public int MaxTripDays { get; set; }

  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Messaging.Email
{
  public interface IEmailRepo
  {
    void SendEmail(EmailMessage message);
  }
}

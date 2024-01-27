using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Movie.Core.Messaging.Sms
{
    public interface IWebClient
    {
        string DoRequest(string endpoint, string method = "GET", string body = null, Dictionary<string, string> headers = null, string contentType = null, X509Certificate clientCertificate = null);
    }
}

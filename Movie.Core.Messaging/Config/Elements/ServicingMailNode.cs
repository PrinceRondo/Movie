using System.Configuration;

namespace Movie.Core.Messaging.Config.Elements
{
    public class ServicingMailNode : ConfigurationElement
    {
        [ConfigurationProperty("emailto", IsRequired = true)]
        public string Emailto
        {
            get { return (string)this["emailto"]; }
            set { this["emailto"] = string.IsNullOrEmpty(value) ? null : value; }
        }
    }
}

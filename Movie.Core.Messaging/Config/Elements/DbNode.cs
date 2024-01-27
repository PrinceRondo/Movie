using System.Configuration;

namespace Movie.Core.Messaging.Config.Elements
{
    public class DbNode : ConfigurationElement
    {
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)this["connectionString"]; }
            set { this["connectionString"] = string.IsNullOrEmpty(value) ? null : value; }
        }
    }
}

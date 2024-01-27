using Movie.Core.Messaging.Config.Elements;
using System.Configuration;

namespace Movie.Core.Messaging.Config
{
    public class SmtpConfig : ConfigurationSection
    {
        public static SmtpConfig Get()
        {
            return (SmtpConfig)ConfigurationManager.GetSection("smtpConfig");
        }

        [ConfigurationProperty("from", IsDefaultCollection = false)]
        public string From
        {
            get { return (string)this["from"]; }
            set { this["from"] = value; }
        }

        [ConfigurationProperty("smtpNode", IsDefaultCollection = false)]
        public SmtpNode SmtpNode
        {
            get { return (SmtpNode)this["smtpNode"]; }
            set { this["smtpNode"] = value; }
        }

        [ConfigurationProperty("dbNode", IsDefaultCollection = false)]
        public DbNode DbNode
        {
            get { return (DbNode)this["dbNode"]; }
            set { this["dbNode"] = value; }
        }

        [ConfigurationProperty("servicingMailNode", IsDefaultCollection = false)]
        public ServicingMailNode ServicingMailNode
        {
            get { return (ServicingMailNode)this["servicingMailNode"]; }
            set { this["servicingMailNode"] = value; }
        }
    }
}

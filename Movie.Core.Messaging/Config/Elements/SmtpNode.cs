﻿using System.Configuration;
using System.IO;

namespace Movie.Core.Messaging.Config.Elements
{
    public class SmtpNode : ConfigurationElement
    {
        [ConfigurationProperty("email", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["email"]; }
            set { this["email"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["host"]; }
            set { this["host"] = value; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("ssl", DefaultValue = false, IsRequired = false)]
        public bool UseSsl
        {
            get { return (bool)this["ssl"]; }
            set { this["ssl"] = value; }
        }

        [ConfigurationProperty("retry", DefaultValue = 1, IsRequired = false)]
        public int MaxRetry
        {
            get { return (int)this["retry"]; }
            set { this["retry"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = @"C:\Workspace\Email", IsRequired = false)]
        public string Path
        {
            get
            {
                var filePath = (string)this["path"];

                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }

                return filePath;
            }
            set { this["filepath"] = value; }
        }
    }
}
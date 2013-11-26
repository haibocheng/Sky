using System;
using System.Configuration;

namespace GreedyToolkit.Aliyun
{
    public class AliyunSection : ConfigurationSection
    {
        private AliyunSection()
        {
        }

        public static AliyunSection GetInstance()
        {
            return ConfigurationManager.GetSection("aliyun") as AliyunSection;
        }

        [ConfigurationProperty("accesskey")]
        public AccessKey AccessKey
        {
            get
            {
                return (AccessKey)this["accesskey"];
            }
            set
            {
                this["accesskey"] = value;
            }
        }

        [ConfigurationProperty("oss")]
        public OSS Oss
        {
            get
            {
                return (OSS)this["oss"];
            }
            set
            {
                this["oss"] = value;
            }
        }
    }

    public class AccessKey : ConfigurationElement
    {
        [ConfigurationProperty("id")]
        public string Id
        {
            get
            {
                return (string)this["id"];
            }
            set
            {
                this["id"] = value;
            }
        }

        [ConfigurationProperty("secret")]
        public string Secret
        {
            get
            {
                return (string)this["secret"];
            }
            set
            {
                this["secret"] = value;
            }
        }
    }

    public class OSS : ConfigurationElement
    {
        [ConfigurationProperty("bucket")]
        public string Bucket
        {
            get
            {
                return (string)this["bucket"];
            }
            set
            {
                this["bucket"] = value;
            }
        }

        [ConfigurationProperty("org")]
        public string Org
        {
            get
            {
                return (string)this["org"];
            }
            set
            {
                this["org"] = value;
            }
        }

        [ConfigurationProperty("mode", DefaultValue = "in")]
        public string Mode
        {
            get
            {
                return (string)this["mode"];
            }
            set
            {
                this["mode"] = value;
            }
        }
    }
}

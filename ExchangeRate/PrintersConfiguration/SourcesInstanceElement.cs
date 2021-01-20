using System.Configuration;

namespace ExchangeRate.SourcesConfiguration
{
    public class SourcesInstanceElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                return (string)base["key"];
            }
            set
            {
                base["key"] = value;
            }
        }

        [ConfigurationProperty("link", IsRequired = true)]
        public string Link
        {
            get
            {
                return (string)base["link"];
            }
            set
            {
                base["link"] = value;
            }
        }

        [ConfigurationProperty("searchedProperty", IsRequired = true)]
        public string SearchedProperty
        {
            get
            {
                return (string)base["searchedProperty"];
            }
            set
            {
                base["searchedProperty"] = value;
            }
        }
    }
}

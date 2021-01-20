using System.Configuration;

namespace ExchangeRate.SourcesConfiguration
{
    public class SourcesConfig : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(SourcesInstanceCollection))]
        public SourcesInstanceCollection SourcesInstances
        {
            get
            {
                return (SourcesInstanceCollection)this["instances"];
            }
        }
    }
}

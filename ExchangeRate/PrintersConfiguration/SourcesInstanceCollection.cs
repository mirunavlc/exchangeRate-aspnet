using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.SourcesConfiguration
{
    public class SourcesInstanceCollection : ConfigurationElementCollection
    {

        public SourcesInstanceElement this[int index]
        {
            get
            {
                return (SourcesInstanceElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        public new SourcesInstanceElement this[string key]
        {
            get
            {
                return (SourcesInstanceElement)BaseGet(key);
            }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));

                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SourcesInstanceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SourcesInstanceElement)element).Key;
        }
    }
}

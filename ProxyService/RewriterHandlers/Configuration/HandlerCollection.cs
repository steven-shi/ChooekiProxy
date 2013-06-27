using System.Configuration;

namespace ProxyService.RewriterHandlers.Configuration
{
	public class HandlerCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new Handle();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Handle)element).ContentType;
		}

		public Handle this[int index]
		{
			get 
			{
				return (Handle)base.BaseGet(index);
			}
		}

		public new Handle this[string key]
		{
			get 
			{
				return (Handle)base.BaseGet(key);
			}
		}
	}
}

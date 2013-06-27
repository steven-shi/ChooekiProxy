using System.Configuration;

namespace ProxyService.RewriterHandlers.Configuration
{
	public class RewriteRuleCollection : System.Configuration.ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new RewriteRule();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RewriteRule)element).Name;
		}

		public RewriteRule this[int index]
		{
			get
			{
				return (RewriteRule)base.BaseGet(index);
			}
		}

		public new RewriteRule this[string key]
		{
			get 
			{
				return (RewriteRule)base.BaseGet(key);
			}
		}
	}
}

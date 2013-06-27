using System.Configuration;

namespace ProxyService.RewriterHandlers.Configuration
{
	public class RewriteHandlerConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("handles", IsDefaultCollection = true)]
		[ConfigurationCollection(typeof(HandlerCollection), AddItemName="handle")]
		public HandlerCollection Handles
		{
			get
			{
				return (HandlerCollection)this["handles"];
			}
		}
	}
}

namespace ProxyService.RewriterHandlers.Configuration
{
	public class RewriteRule : System.Configuration.ConfigurationElement
	{
		[System.Configuration.ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)this["name"];
			}
			set
			{
				this["name"] = value;
			}
		}


		[System.Configuration.ConfigurationProperty("match",IsRequired=true)]
		public Match Match
		{
			get
			{
				return (Match)this["match"];
			}
			set
			{
				this["match"] = value;
			}
		}

		[System.Configuration.ConfigurationProperty("replace",IsRequired=true)]
		public Replace Replace
		{
			get
			{
				return (Replace)this["replace"];
			}
			set
			{
				this["replace"] = value;
			}
		}
	}
}

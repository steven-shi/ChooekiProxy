using System.Configuration;

namespace ProxyService.RewriterHandlers.Configuration
{
	public class Handle : System.Configuration.ConfigurationElement
	{

		[ConfigurationProperty("content-type",IsKey=true,IsRequired=true)]
		public string ContentType
		{
			get
			{
				return this["content-type"].ToString();
			}
			set
			{
				this["content-type"] = value;
			}
		}

		[ConfigurationProperty("type",IsRequired=true)]
		public string TypeFullName
		{
			get
			{
				return this["type"].ToString();
			}
			set
			{
				this["type"] = value;
			}
		}


		[ConfigurationProperty("",IsDefaultCollection=true)]
		[ConfigurationCollection(typeof(RewriteRuleCollection), AddItemName = "rewrite")]
		public RewriteRuleCollection RewriteRules
		{
			get
			{
				return (RewriteRuleCollection)this[""];
			}
			set
			{
				this[""] = value;
			}
		}

		//[ConfigurationProperty("content-type")]
		//public string ContentType
		//{
		//    get
		//    {
		//        return this["content-type"].ToString();
		//    }
		//    set
		//    {
		//        this["content-type"] = value;
		//    }
		//}

		//[ConfigurationProperty("type")]
		//public string TypeFullName
		//{
		//    get
		//    {
		//        return this["type"].ToString();
		//    }
		//    set
		//    {
		//        this["type"] = value;
		//    }
		//}
	}
}

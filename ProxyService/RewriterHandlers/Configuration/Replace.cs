namespace ProxyService.RewriterHandlers.Configuration
{
	public class Replace : System.Configuration.ConfigurationElement
	{
		public Replace()
		{
			
		}

		public Replace(string innerText)
		{
			this.innerText = innerText;
		}

		private string innerText;

		protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
		{
			innerText = reader.ReadElementContentAsString().Trim();
		}

		public override string ToString()
		{
			return innerText;
		}
	}
}

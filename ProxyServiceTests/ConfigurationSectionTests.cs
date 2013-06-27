using System;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyService.RewriterHandlers.Configuration;

namespace ProxyServiceTests
{
	[TestClass]
	public class ConfigurationSectionTests
	{
		[TestMethod]
		public void TestRewriteHandleConfiguration()
		{
			var _section = (RewriteHandlerConfigurationSection)
				ConfigurationManager.GetSection("RewriteHandleConfiguration");
			Assert.AreEqual(1, _section.Handles.Count);
			Assert.AreEqual(2, _section.Handles[0].RewriteRules.Count);
			Assert.AreEqual("href=[\"'](.*?)['\"]", _section.Handles[0].RewriteRules[0].Match.ToString());
			Assert.AreEqual("href=\"/?t={host}$1", _section.Handles[0].RewriteRules[0].Replace.ToString());
		}
	}
}

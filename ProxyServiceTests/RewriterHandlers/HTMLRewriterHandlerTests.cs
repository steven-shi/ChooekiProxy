using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyService.RewriterHandlers;
using ProxyService.RewriterHandlers.Configuration;

namespace ProxyServiceTests.RewriterHandlers
{
	[TestClass]
	public class HTMLRewriterHandlerTests
	{
		MemoryStream ms = new MemoryStream();
		private StreamReader sr;

		[TestMethod]
		public void TestHrefCase1()
		{
			var result = TestHTMLRewrite("<a href=\"/intl/en/ads/\">Advertising&nbsp;Programmes</a>",
			                new Uri("http://www.chooeki.info"));
			Assert.AreEqual("<a href=\"/?t=http://www.chooeki.info/intl/en/ads/\">Advertising&nbsp;Programmes</a>", result);
		}

		[TestMethod]
		public void TestHrefCase2()
		{
			var result = TestHTMLRewrite("<a href='/intl/en/ads/'>Advertising&nbsp;Programmes</a>",
							new Uri("http://www.chooeki.info"));
			Assert.AreEqual("<a href=\"/?t=http://www.chooeki.info/intl/en/ads/\">Advertising&nbsp;Programmes</a>", result);
		}

		[TestMethod]
		public void TestHref_With_Slash()
		{
			var result = TestHTMLRewrite("<a href='/intl/en/ads/'>Advertising&nbsp;Programmes</a>",
							new Uri("http://www.chooeki.info"));
			Assert.AreEqual("<a href=\"/?t=http://www.chooeki.info/intl/en/ads/\">Advertising&nbsp;Programmes</a>", result);
		}

		[TestMethod]
		public void TestHref_Without_Slash()
		{
			var result = TestHTMLRewrite("<a href='intl/en/ads/'>Advertising&nbsp;Programmes</a>",
							new Uri("http://www.chooeki.info"));
			Assert.AreEqual("<a href=\"/?t=http://www.chooeki.info/intl/en/ads/\">Advertising&nbsp;Programmes</a>", result);
		}

		[TestMethod]
		public void TestHref_Without_Http()
		{
			var result = TestHTMLRewrite("<a href='http://www.test.com/intl/en/ads/'>Advertising&nbsp;Programmes</a>",
							new Uri("http://www.chooeki.info"));
			Assert.AreEqual("<a href=\"/?t=http://www.test.com/intl/en/ads/\">Advertising&nbsp;Programmes</a>", result);
		}


		private string TestHTMLRewrite(string source, Uri contextUri)
		{
			var _section = (RewriteHandlerConfigurationSection)ConfigurationManager.GetSection("RewriteHandleConfiguration");

			HTMLRewriterHandler target = new HTMLRewriterHandler(_section.Handles["test-type"].RewriteRules);
			var stream = target.Rewrite(GetStream(source), contextUri);
			stream.Seek(0, SeekOrigin.Begin);
			sr = new StreamReader(stream);
			var result = sr.ReadToEnd();
			sr.Close();
			return result;
		}

		private Stream GetStream(string html)
		{
			byte[] content = Encoding.Default.GetBytes(html);

			ms.Write(content, 0, content.Length);
			ms.Seek(0, SeekOrigin.Begin);
			return ms;
		}
	}
}

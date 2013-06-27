using ProxyService.UriHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProxyServiceTests
{
    /// <summary>
    ///This is a test class for UriRewriterTest and is intended
    ///to contain all UriRewriterTest Unit Tests
    ///</summary>
	[TestClass()]
	public class UriRewriterTest
	{
		/// <summary>
		///A test for GetProxyUri
		///</summary>
		[TestMethod()]
		public void GetProxyUriTest()
		{
			string queryName = "t";
			UriRewriter target = new UriRewriter(queryName);
			Uri targetUri = new Uri("http://localhost:3789/?t=http://www.google.com");
			Uri referer = null;
			Uri expected = new Uri("http://www.google.com");
			Uri actual;
			actual = target.GetProxyUri(targetUri, referer);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void GetProxyUriInlineUriTest()
		{
			string queryName = "t";
			UriRewriter target = new UriRewriter(queryName);
			Uri targetUri = new Uri("http://localhost:3789/logo.png");
			Uri referer = new Uri("http://localhost:3789/?t=http://www.google.com/");
			Uri expected = new Uri("http://www.google.com/logo.png");
			Uri actual;
			actual = target.GetProxyUri(targetUri, referer);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		/// The case referer url is refering to proxy url
		/// TODO tolerate this?
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetProxyUriInlineUriTest2()
		{
			string queryName = "t";
			UriRewriter target = new UriRewriter(queryName);
			Uri targetUri = new Uri("http://localhost:3789/logo.png");
			Uri referer = new Uri("http://www.google.com/"); //invalid case
			Uri expected = new Uri("http://www.google.com/logo.png");
			target.GetProxyUri(targetUri, referer);
		}

		/// <summary>
		/// Inline url but no refer
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetProxyUriInlineUriTest3()
		{
			string queryName = "t";
			UriRewriter target = new UriRewriter(queryName);
			Uri targetUri = new Uri("http://localhost:3789/logo.png");
			Uri referer = null;
			Uri expected = new Uri("http://www.google.com/logo.png");
			target.GetProxyUri(targetUri, referer);
		}
	}
}

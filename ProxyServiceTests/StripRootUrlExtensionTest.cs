using ProxyService.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProxyServiceTests
{
    
    
    /// <summary>
    ///This is a test class for StripRootUrlExtensionTest and is intended
    ///to contain all StripRootUrlExtensionTest Unit Tests
    ///</summary>
	[TestClass()]
	public class StripRootUrlExtensionTest
	{
		/// <summary>
		///A test for StripRootUrl
		///</summary>
		[TestMethod()]
		public void StripRootUrlTest1()
		{
			string url = "/proxy/?t=http://www.google.com";
			string root = "/proxy";
			string expected = "/?t=http://www.google.com";
			string actual;
			actual = StripRootUrlExtension.StripRootUrl(url, root);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for StripRootUrl
		///</summary>
		[TestMethod()]
		public void StripRootUrlTest()
		{
			string root = "/proxy";

			Assert.AreEqual(new Uri("http://localhost/proxy/images/srpr/nav_logo27.png").StripRootUrl(root),
				"http://localhost/images/srpr/nav_logo27.png");

			Assert.AreEqual(new Uri("http://localhost/proxy/").StripRootUrl(root),
				"http://localhost/");

			Assert.AreEqual(new Uri("http://localhost/proxy/images").StripRootUrl(root),
				"http://localhost/images");


			Assert.AreEqual(new Uri("http://localhost/proxy/images/").StripRootUrl(root),
				"http://localhost/images/");

			Assert.AreEqual(new Uri("http://localhost/images/").StripRootUrl(root),
				"http://localhost/images/");

			Assert.AreEqual(new Uri("http://localhost/proxy").StripRootUrl(root),
				"http://localhost");

			Assert.AreEqual(new Uri("https://localhost/proxy/images/srpr/nav_logo27.png").StripRootUrl(root),
				"https://localhost/images/srpr/nav_logo27.png");

		}

		/// <summary>
		///A test for StripRootUrl
		///</summary>
		[TestMethod()]
		public void StripRootUrlTest2()
		{
			string root = "";

			Assert.AreEqual(new Uri("http://localhost/proxy/images/srpr/nav_logo27.png").StripRootUrl(root),
				"http://localhost/proxy/images/srpr/nav_logo27.png");

			Assert.AreEqual(new Uri("http://localhost/proxy/").StripRootUrl(root),
				"http://localhost/proxy/");

			Assert.AreEqual(new Uri("http://localhost/proxy/images").StripRootUrl(root),
				"http://localhost/proxy/images");


			Assert.AreEqual(new Uri("http://localhost/proxy/images/").StripRootUrl(root),
				"http://localhost/proxy/images/");

			Assert.AreEqual(new Uri("http://localhost/images/").StripRootUrl(root),
				"http://localhost/images/");

			Assert.AreEqual(new Uri("http://localhost/proxy").StripRootUrl(root),
				"http://localhost/proxy");

			Assert.AreEqual(new Uri("https://localhost/proxy/images/srpr/nav_logo27.png").StripRootUrl(root),
				"https://localhost/proxy/images/srpr/nav_logo27.png");

			Assert.AreEqual(StripRootUrlExtension.StripRootUrl((Uri)null,root),
				null);

		}
	}
}

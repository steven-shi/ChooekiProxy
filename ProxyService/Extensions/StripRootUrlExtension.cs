using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyService.Extensions
{
	public static class StripRootUrlExtension
	{
		/// <summary>
		/// strip the root url from the url in order to have the real proxy url
		/// e.g. 
		/// /proxy/?t=http://www.google.com
		/// so need to strip the root like
		/// /?t=http://www.google.com
		/// </summary>
		/// <param name="pathAndQuery">original url</param>
		/// <param name="root">the url that needs to strip</param>
		/// <returns>stripped url</returns>
		public static string StripRootUrl(this string pathAndQuery, string root)
		{
			if (pathAndQuery.ToLower().StartsWith(root.ToLower()))
			{
				return pathAndQuery.Substring(root.Length);
			}
			return pathAndQuery;
		}

		/// <summary>
		/// strip the root url from the url in order to have the real proxy url
		/// e.g. 
		/// /proxy/?t=http://www.google.com
		/// so need to strip the root like
		/// /?t=http://www.google.com
		/// </summary>
		/// <param name="uri">original url</param>
		/// <param name="root">the url that needs to strip</param>
		/// <returns>stripped url</returns>
		public static Uri StripRootUrl(this Uri uri, string root)
		{
			if (uri == null)
				return null;
			string url = uri.PathAndQuery.StripRootUrl(root);
			
			return new Uri(string.Format("{0}://{1}{2}",uri.Scheme,uri.Host,url));
		}
	}
}

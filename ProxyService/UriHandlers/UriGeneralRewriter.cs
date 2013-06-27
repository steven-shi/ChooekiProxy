using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyService.UriHandlers
{
	public class UriGeneralRewriter : IUriRewriter
	{
		private string _queryName;

		public UriGeneralRewriter(string queryName)
		{
			_queryName = queryName;
		}

		public Uri GetProxyUri(Uri targetUri, Uri referer)
		{
			var queryString = System.Web.HttpUtility.ParseQueryString(targetUri.Query);

			if (queryString.Count == 1 && queryString[_queryName] != null) //only has one query and it's a rewrite url query?
			{
				return RewriteUriFromQuery(targetUri, queryString[_queryName]);
			}

			return targetUri;
		}

		protected Uri RewriteUriFromQuery(Uri targetUri, string queryString)
		{
			try
			{
				Uri proxyUri = new Uri(System.Web.HttpUtility.UrlDecode(queryString));
				return proxyUri;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Failed to cast proxy Uri", ex);
			}
		}
	}
}

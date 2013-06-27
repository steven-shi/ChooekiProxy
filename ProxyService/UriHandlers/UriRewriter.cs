using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyService.UriHandlers
{
	public class UriRewriter : IUriRewriter
	{
		private readonly string _queryName;

		public UriRewriter(string queryName)
		{
			_queryName = queryName;
		}

		public Uri GetProxyUri(Uri targetUri, Uri referer)
		{
			if (!string.IsNullOrEmpty(targetUri.Query)) //in page url? e.g. /test
			{
				var queryString = System.Web.HttpUtility.ParseQueryString(targetUri.Query);
				if (queryString.Count == 1 && queryString[_queryName] != null) //only has one query and it's a rewrite url query?
				{
					return RewriteUriFromQuery(targetUri, queryString[_queryName]);
				}
			}

			return RewriteForInlineUri(targetUri, referer);
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

		protected Uri RewriteForInlineUri(Uri targetUri, Uri referer)
		{
			try
			{
				if (referer == null)
					throw new InvalidOperationException("Proxying for in-site url, however, refer is null");

				referer = GetProxyUri(referer, null); // the referer url should be formatted with ?t=xxxx
				// request in-site resource? try to get refer
				if (referer == null)
					throw new InvalidOperationException("Invalid referer url");

				string newUri = string.Format("{0}://{1}{2}", referer.Scheme, referer.Host, targetUri.PathAndQuery);

				Uri proxyUri = new Uri(newUri);
				return proxyUri;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Failed to cast proxy Uri", ex);
			}
		}
	}
}

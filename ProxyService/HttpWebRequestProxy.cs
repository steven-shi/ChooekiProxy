using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ProxyService.Extensions;
using ProxyService.UriHandlers;

namespace ProxyService
{
	public class HttpWebRequestProxy
	{
		private static IUriRewriter _uriRewriter;
		private readonly string _rootPath;

		/// <summary>
		/// Constructor of HttpWebRequest Proxy
		/// </summary>
		/// <param name="uriRewriter">uri rewriter component</param>
		/// <param name="rootPath">root url of current application, which needs to be excluded from proxying url</param>
		public HttpWebRequestProxy(IUriRewriter uriRewriter, string rootPath)
		{
			_uriRewriter = uriRewriter;
			_rootPath = rootPath;
		}

		public HttpWebRequest Handle(HttpRequest request)
		{
			var proxyUri = _uriRewriter.GetProxyUri(request.Url.StripRootUrl(_rootPath), request.UrlReferrer.StripRootUrl(_rootPath));

			Instrument.Logger.WriteMessage(string.Format("Proxying for: {0}", proxyUri), TraceEventType.Information);

			return CopyHttpRequest(request, proxyUri);
		}

		private HttpWebRequest CopyHttpRequest(HttpRequest request, Uri uri)
		{
			HttpWebRequest proxyRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
			//copy cookie
			//try
			//{
			//    foreach (var cookieName in request.Cookies)
			//    {
			//        HttpCookie cookie = request.Cookies[cookieName.ToString()];
			//        if (proxyRequest.CookieContainer == null)
			//            proxyRequest.CookieContainer = new CookieContainer();
			//        var proxyCookie = new System.Net.Cookie(cookie.Name, cookie.Value) { Domain = uri.Host };
			//        proxyRequest.CookieContainer.Add(proxyCookie);
			//    }
			//}
			//catch(Exception ex)
			//{
			//    throw;
			//}

			//copy Accept
			proxyRequest.Accept = ConvertToCommaString(request.AcceptTypes);
			//copy User-Agent
			proxyRequest.UserAgent = request.UserAgent;
			//Accept-Language
			CopyHeader(request, proxyRequest, "Accept-Language");
			//Accept-Encoding
			CopyHeader(request, proxyRequest, "Accept-Encoding");
			//Accept-Charset
			CopyHeader(request, proxyRequest, "Accept-Charset");
			//Keep-Alive
			CopyHeader(request, proxyRequest, "Keep-Alive");
			//Connection
			//CopyHeader(request, proxyRequest, "Connection");

			//refere
			//if(request.UrlReferrer!=null)
			//    proxyRequest.Referer = _uriRewriter.GetProxyUri(request.UrlReferrer, null).ToString();

			//Copy http method
			proxyRequest.Method = request.HttpMethod;
			proxyRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			//Copy stream
			if(proxyRequest.Method=="POST")
				request.InputStream.CopyTo(proxyRequest.GetRequestStream());


			return proxyRequest;
		}

		private string ConvertToCommaString(string[] input)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in input)
			{
				if (sb.Length!=0)
					sb.Append(",");
				sb.Append(s);
			}
			
			if(sb.Length !=0)
				sb.Append(";");

			return sb.ToString();
		}

		private void CopyHeader(HttpRequest request, HttpWebRequest proxyRequest, string headerName)
		{
			if(request.Headers[headerName]!=null)
				proxyRequest.Headers[headerName] = request.Headers[headerName];
		}
	}
}

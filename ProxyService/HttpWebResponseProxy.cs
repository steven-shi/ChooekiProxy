using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.IO;
using ProxyService.Extensions;
using ProxyService.RewriterHandlers;

namespace ProxyService
{
	public class HttpWebResponseProxy
	{
		private readonly RewriteHandlerMapping _mapping;

		public HttpWebResponseProxy(RewriteHandlerMapping mapping)
		{
			_mapping = mapping;
		}

		public void Handle(HttpWebResponse proxyResponse, HttpResponse contextResponse)
		{
			//add cookie
			//foreach (var cookieName in proxyResponse.Cookies)
			//{
			//    var proxyCookie = proxyResponse.Cookies[cookieName.ToString()];

			//    if(proxyCookie.Name.StartsWith("Proxy_"))
			//        contextResponse.SetCookie(new HttpCookie(proxyCookie.Name, proxyCookie.Value));
			//}
			//contextResponse.SetCookie(new HttpCookie("proxyHost", proxyResponse.ResponseUri.Host));
			
			//add Content-Type
			contextResponse.ContentType = proxyResponse.ContentType;
			//contextResponse
			//add Content-Length
			//Content-Encoding
			//contextResponse.Headers["Content-Encoding"] = proxyResponse.ContentEncoding;

			//copy stream 
			using (Stream responseStream = proxyResponse.GetResponseStream())
			{
				Stream rwStream = RewriteStream(responseStream,proxyResponse.ResponseUri, proxyResponse.ContentType);
				//contextResponse.Headers["Content-Length"] = rwStream.Length.ToString();
				rwStream.CopyToStream(contextResponse.OutputStream);
				rwStream.Close();
			}
		}

		private Stream RewriteStream(Stream proxyStream, Uri streamUri, string contentType)
		{
			//return proxyStream;
			int indexOfsemi = contentType.IndexOf(";");
			if (indexOfsemi < 0)
				indexOfsemi = contentType.Length;

			string contentTypeId = contentType.Substring(0, indexOfsemi);

			var rewriter = _mapping[contentTypeId];
			if (rewriter == null)
				return proxyStream;
			return rewriter.Rewrite(proxyStream, streamUri);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyService.UriHandlers
{
	public interface IUriRewriter
	{
		Uri GetProxyUri(Uri targetUri, Uri referer);
	}
}

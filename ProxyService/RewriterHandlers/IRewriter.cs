using System;
using System.IO;
using System.Text;

namespace ProxyService.RewriterHandlers
{
	public interface IRewriter
	{
		Stream Rewrite(Stream proxyStream, Uri streamUri);
		Stream Rewrite(Stream proxyStream, Uri streamUri, Encoding encoding);
	}
}

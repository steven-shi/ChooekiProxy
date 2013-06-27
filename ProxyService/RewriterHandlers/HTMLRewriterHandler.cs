using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ProxyService.RewriterHandlers.Configuration;

namespace ProxyService.RewriterHandlers
{
	public class HTMLRewriterHandler : IRewriter
	{
		private readonly RewriteRuleCollection _rules;

		public HTMLRewriterHandler(RewriteRuleCollection rules)
		{
			_rules = rules;
		}

		public Stream Rewrite(Stream proxyStream, Uri streamUri)
		{
			return Rewrite(proxyStream, streamUri, Encoding.Default);
		}

		public Stream Rewrite(Stream proxyStream, Uri streamUri, Encoding encoding)
		{
			string content;
			using (StreamReader sr = new StreamReader(proxyStream, encoding))
			{
				content = sr.ReadToEnd();
			}
			foreach (RewriteRule rule in _rules)
			{
				content = RewriteRule(rule, streamUri, content);
			}

			byte[] buffer = encoding.GetBytes(content);
			MemoryStream ms = new MemoryStream();
			ms.Write(buffer, 0, buffer.Length);

			return ms;
		}

		private string RewriteRule(RewriteRule rule, Uri contextUri, string content)
		{
			var specRule = rule.ReplaceToken(contextUri);

			Regex regex = new Regex(specRule.Match.ToString(), RegexOptions.IgnoreCase);
			var replacedContent = regex.Replace(content, specRule.Replace.ToString());

			return replacedContent;
		}
	}
}

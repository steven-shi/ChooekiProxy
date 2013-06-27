using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyService.RewriterHandlers.Configuration;

namespace ProxyService.RewriterHandlers
{
	public static class TokenManager
	{
		//todo require refactor
		public static RewriteRule ReplaceToken(this RewriteRule tokenRule, Uri contextUri)
		{
			var match = tokenRule.Match.ToString();
			var replace = tokenRule.Replace.ToString();
			if (!match.Contains("{") && !replace.Contains("{")) // no token inside, quick skip
				return tokenRule;

			RewriteRule rule = new RewriteRule();
			rule.Match = new Match(StringReplace(match, contextUri));
			rule.Replace = new Replace(StringReplace(replace, contextUri));

			return rule;
		}

		private static string StringReplace(string original, Uri contextUri)
		{
			return original.Replace("{host}", string.Format("{0}://{1}", contextUri.Scheme, contextUri.DnsSafeHost));
		}
	}
}

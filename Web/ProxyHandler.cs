using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Instrument;

namespace Web
{
	public class ProxyHandler : IHttpHandler
	{
		#region IHttpHandler Members

		public bool IsReusable
		{
			// Return false in case your Managed Handler cannot be reused for another request.
			// Usually this would be false in case you have some state information preserved per request.
			get { return true; }
		}

		public void ProcessRequest(HttpContext context)
		{
			Logger.WriteMessage("CurrentURL: " + context.Request.Url.PathAndQuery, TraceEventType.Information);

			//if (MatchRealPage(context.Request.Url.PathAndQuery))
			//{
			//    context.Response.
			//}

			try
			{
				Logger.WriteMessage(string.Format("Request from IP: {0}", context.Request.UserHostAddress), TraceEventType.Information);
				HttpWebRequest request = Global.proxyRequestHandler.Handle(context.Request);
				HttpWebResponse proxyResponse = (HttpWebResponse)request.GetResponse();
				Global.proxyResponseHandler.Handle(proxyResponse, context.Response);
				context.Response.Flush();
				
				context.Response.End();
			}
			catch (System.Threading.ThreadAbortException ex)
			{
				//can be ignored
			}
			catch (Exception ex)
			{
				Logger.WriteMessage(ex.ToString(), TraceEventType.Error);
				context.Response.StatusCode = 500;
				context.Response.End();
				//throw;
			}
		}

		private bool MatchRealPage(string pathAndQuery)
		{
			string regexPattern = ConfigurationManager.AppSettings["ExcludeRoutingPattern"];
			Regex regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
			return regex.IsMatch(pathAndQuery);
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Instrument;

namespace Web
{
	public class ProxyModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(context_BeginRequest);
		}

		void context_BeginRequest(object sender, EventArgs e)
		{
			HttpContext context = HttpContext.Current;

			Logger.WriteMessage("CurrentURL: " + context.Request.Url.PathAndQuery, TraceEventType.Information);

			//if (MatchRealPage(context.Request.Url.PathAndQuery))
			//{
			//    context.Response.
			//}
			if (context.Request.Url.Query == "")
			{
				return;
			}


			try
			{
				Logger.WriteMessage(string.Format("Request from IP: {0}", context.Request.UserHostAddress), TraceEventType.Information);
				HttpWebRequest request = Global.proxyRequestHandler.Handle(context.Request);
				HttpWebResponse proxyResponse = (HttpWebResponse)request.GetResponse();
				Global.proxyResponseHandler.Handle(proxyResponse, context.Response);

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

		

		public void Dispose()
		{
			
		}
	}
}

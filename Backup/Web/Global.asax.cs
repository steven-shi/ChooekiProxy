using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Instrument;
using log4net;
using ProxyService;
using ProxyService.RewriterHandlers;
using ProxyService.UriHandlers;

namespace Web
{
	public class Global : System.Web.HttpApplication
	{
		public static HttpWebRequestProxy proxyRequestHandler;
		public static HttpWebResponseProxy proxyResponseHandler;
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		protected void Application_Start(object sender, EventArgs e)
		{
			Logger.log = log; //TODO need to refactor
			Logger.WriteMessage("Application starting", System.Diagnostics.TraceEventType.Start);
			string rootUrl = ConfigurationManager.AppSettings["root"];
			// initalize proxy services
			proxyRequestHandler = new HttpWebRequestProxy(new UriGeneralRewriter("t"), rootUrl);
			proxyResponseHandler = new HttpWebResponseProxy(new RewriteHandlerMapping());
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}
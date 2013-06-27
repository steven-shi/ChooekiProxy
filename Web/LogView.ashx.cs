using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Instrument;

namespace Web
{
	public class LogView : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			var path = context.Server.MapPath("./Logs/rolling.txt");
			try
			{
				using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					StreamReader reader = new StreamReader(fs);
					string log = reader.ReadToEnd();
					context.Response.ContentType = "text/plain";
					context.Response.Write(log);
				}
			}
			catch (Exception ex)
			{
				context.Response.Write(ex.ToString());
			}

		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}

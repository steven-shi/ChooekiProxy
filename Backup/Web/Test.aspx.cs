using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Instrument;

namespace Web
{
	public partial class Test : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			using (System.IO.StreamWriter sw = new System.IO.StreamWriter
				(@"D:\Hosting\6933829\html\proxy\logs\rolling.txt"))
			{
				sw.WriteLine(DateTime.Now.ToString());
			}
			Logger.WriteMessage("TestPage",System.Diagnostics.TraceEventType.Information);
			Response.Write("Test written");
		}
	}
}
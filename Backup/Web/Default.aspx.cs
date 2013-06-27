using System;

namespace Web
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void GoButton_Click(object sender, EventArgs e)
		{
			try
			{
				Uri uri = new Uri(AddressTB.Text);
				Response.Redirect(string.Format("/?t={0}", Server.UrlEncode(uri.ToString())));
			}
			catch (UriFormatException ex)
			{
				InfoLabel.Text = string.Format("{0} is not a valid url", AddressTB.Text);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProxyService.Extensions
{
	public static class CopyStream
	{
		public static void CopyToStream(this Stream input, Stream output)
		{
			if (input.CanSeek == false)
			{
				output = input;
				return;
			}

			byte[] buffer = new byte[32768];
			input.Seek(0, SeekOrigin.Begin);
			
			while (true)
			{
				int read = input.Read(buffer, 0, buffer.Length);
				if (read <= 0)
					return;
				output.Write(buffer, 0, read);
			}
		}
	}
}

using System;
using System.Diagnostics;
using System.IO;
using log4net;

namespace Instrument
{
	public class Logger
	{
		//public Logger()
		//{
		//    log =;
		//}


		public static ILog log = LogManager.GetLogger(typeof(Logger));

		public static void WriteMessage(string message, TraceEventType eventType)
		{
			switch (eventType)
			{
				case TraceEventType.Start:
					log.Debug(message);
					break;
				case TraceEventType.Information:
					log.Info(message);
					break;
				case TraceEventType.Verbose:
					log.Debug(message);
					break;
				case TraceEventType.Warning:
					log.Warn(message);
					break;
				case TraceEventType.Error:
					log.Error(message);
					break;
				default:
					log.Debug(message);
					break;
			}
			//CustomLog(message);

		}


		//private static void CustomLog(string message)
		//{
		//    using(StreamWriter sw = new StreamWriter(@"D:\Hosting\6933829\html\proxy\logs\cus.txt"))
		//    {
		//        sw.WriteLine(DateTime.Now + " " + message);
		//    }

		//}

		#region EntLib logger
		//private static LogWriter logger = new LogWriterFactory().CreateDefault();

		//public static void WriteError(string message)
		//{
		//    LogEntry log = new LogEntry();
		//    log.Categories.Add("General");
		//    log.Message = message;
		//    log.Severity = TraceEventType.Error;

		//    logger.Write(log);
		//}

		//public static void WriteInfo(string message)
		//{
		//    LogEntry log = new LogEntry();
		//    log.Categories.Add("General");
		//    log.Message = message;
		//    log.Severity = TraceEventType.Information;

		//    logger.Write(log);
		//}

		//public static void WriteMessage(string message, TraceEventType eventType)
		//{
		//    LogEntry log = new LogEntry();
		//    log.Categories.Add("General");
		//    log.Message = message;
		//    log.Severity = eventType;

		//    logger.Write(log);
		//} 
		#endregion
	}
}

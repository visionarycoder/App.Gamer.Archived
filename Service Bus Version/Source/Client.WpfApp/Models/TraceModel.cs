using System;

namespace Gamer.Client.WpfApp.Models
{

	public class TraceModel
	{

		public DateTime Timestamp { get; set; }
		public string Source { get; set; }
		public string Action { get; set; }

		public TraceModel()
		{
			
		}

		public TraceModel(DateTime timestamp, string source, string action)
		{
			Timestamp = timestamp;
			Source = source;
			Action = action;
		}

		public override string ToString()
		{
			return $"{Timestamp:G}: {Source} {Action}";
		}
	}

}
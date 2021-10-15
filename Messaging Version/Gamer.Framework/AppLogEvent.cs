namespace Gamer.Framework
{

	// From https://docs.microsoft.com/en-us/dotnet/core/extensions/logging

	public static class AppLogEvent
	{
		
		public const int Create = 1000;
		public const int Read = 1001;
		public const int Update = 1002;
		public const int Delete = 1003;
		
		public const int Details = 3000;
		public const int Error = 3001;
		
		public const int ReadNotFound = 4000;
		public const int UpdateNotFound = 4001;
		
	}

}
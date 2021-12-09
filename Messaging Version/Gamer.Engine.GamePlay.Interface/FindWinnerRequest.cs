using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class FindWinnerRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}
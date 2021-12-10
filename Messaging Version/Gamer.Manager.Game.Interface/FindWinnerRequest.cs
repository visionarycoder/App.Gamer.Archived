using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class FindWinnerRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}
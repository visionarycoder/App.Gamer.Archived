using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class IsGamePlayableRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}
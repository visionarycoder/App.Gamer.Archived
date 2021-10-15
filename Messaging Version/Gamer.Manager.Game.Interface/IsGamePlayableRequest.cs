using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class IsGamePlayableRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}
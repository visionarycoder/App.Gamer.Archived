using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class StartGameResponse : ServiceMessageResponse
	{
		public Guid GameSessionId { get; set; }
	}
}
using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class StartGameResponse : ServiceMessageResponse
	{
		public Guid GameSessionId { get; set; }
	}
}
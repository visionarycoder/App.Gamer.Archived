using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetBoardRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}
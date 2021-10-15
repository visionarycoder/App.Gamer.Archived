using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class FindWinnerRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}
using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class FindWinnerRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}
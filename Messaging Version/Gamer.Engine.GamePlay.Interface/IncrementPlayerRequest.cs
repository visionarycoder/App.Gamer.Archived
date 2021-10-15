using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class IncrementPlayerRequest : ServiceMessageRequest
	{
		public Guid gameSessionId { get; set; }
	}
}
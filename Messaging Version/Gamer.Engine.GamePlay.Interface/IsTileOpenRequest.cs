using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class IsTileOpenRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
		public string Address { get; set; }
	}
}
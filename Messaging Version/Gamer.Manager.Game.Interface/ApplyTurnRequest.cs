using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class ApplyTurnRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
		public Guid PlayerId { get; set; }
		public string Address { get; set; }
		public int Step { get; set; }
	}
}
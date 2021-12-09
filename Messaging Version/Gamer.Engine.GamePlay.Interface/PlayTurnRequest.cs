using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class PlayTurnRequest : ServiceMessageRequest
	{
	
		public Guid GameSessionId { get; set; }
		public Guid PlayerId { get; set; }
		public string Address { get; set; }
		public bool IsAutoPlay { get; set; }

	}
}
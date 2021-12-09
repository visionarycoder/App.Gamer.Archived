using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class StartGameRequest : ServiceMessageRequest
	{
		public Guid GameDefinitionId { get; set; }
		public int PlayerCount { get; set; }
	}
}
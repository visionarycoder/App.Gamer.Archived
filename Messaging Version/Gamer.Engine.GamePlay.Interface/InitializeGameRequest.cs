using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class InitializeGameRequest : ServiceMessageRequest
	{
		public Guid GameDefinitionId { get; set; }
		public int NumberOfPlayers { get; set; }
	}
}
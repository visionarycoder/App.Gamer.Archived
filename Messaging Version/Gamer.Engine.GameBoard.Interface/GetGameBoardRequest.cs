using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Engine.GameBoard.Interface
{
	public class GetGameBoardRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}
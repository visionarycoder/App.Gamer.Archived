using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.Player.Interface
{
	public class GetPlayerRequest : ServiceMessageRequest
	{
		public Guid PlayerId { get; set; }
	}
}
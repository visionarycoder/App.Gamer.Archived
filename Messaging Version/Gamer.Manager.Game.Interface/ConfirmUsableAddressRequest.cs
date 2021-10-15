using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class ConfirmUsableAddressRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
		public string Address { get; set; }
	}
}
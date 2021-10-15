using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Engine.Validation.Interface
{
	public class ValidateUserInputRequest : ServiceMessageRequest
	{

		public Guid GameSessionId { get; set; }
		public string Input { get; set; }

	}
}
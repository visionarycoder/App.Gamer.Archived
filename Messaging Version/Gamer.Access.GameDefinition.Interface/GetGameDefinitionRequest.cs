using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.GameDefinition.Interface
{
	public class GetGameDefinitionRequest : ServiceMessageRequest
	{
		public Guid GameDefinitionId { get; set; }
	}
}
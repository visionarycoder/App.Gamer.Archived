using Gamer.Utility.ServiceMessaging;

using System;

namespace Gamer.Access.GameDefinition.Interface
{
    public class GetGameDefinitionRequest : ServiceMessageRequest
    {
        public Guid GameDefinitionId { get; set; }
    }
}
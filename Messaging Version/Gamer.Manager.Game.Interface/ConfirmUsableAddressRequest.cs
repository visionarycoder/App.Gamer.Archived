using Gamer.Utility.ServiceMessaging;

using System;

namespace Gamer.Manager.Game.Interface
{
    public class ConfirmUsableAddressRequest : ServiceMessageRequest
    {
        public Guid GameSessionId { get; set; }
        public string Address { get; set; }
    }
}
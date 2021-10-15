using System;
using System.Diagnostics;

namespace Gamer.Framework.ServiceMessaging
{
    public abstract class ServiceMessageRequest : IServiceMessageRequest
    {
        public Guid CorrelationId { get; set; }
        public Guid MessageId { get; set; }
        public DateTime Timestamp { get; set; }
        public Stopwatch Stopwatch { get; set; }
    }
}
using System;
using System.Diagnostics;

namespace Util.ServiceMessaging
{

    public abstract class ServiceMessageRequest : IServiceMessageRequest
    {
        public Guid CorrelationId { get; init; }
        public Guid MessageId { get; init; }
        public DateTime Timestamp { get; init; }
        public Stopwatch Stopwatch { get; init; }
    }

}
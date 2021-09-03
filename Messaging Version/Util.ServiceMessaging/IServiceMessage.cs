using System;
using System.Diagnostics;

namespace Util.ServiceMessaging
{

    public interface IServiceMessage
    {

        Guid CorrelationId { get; init; }
        Guid MessageId { get; init; }
        DateTime Timestamp { get; init; }
        Stopwatch Stopwatch { get; init; }
    }

}
using System;
using System.Diagnostics;

namespace Util.ServiceMessaging
{
    public static class ServiceMessageFactory<T> where T : IServiceMessage, new()
    {

        public static T Create()
        {
            return Create(Guid.Empty);
        }

        public static T Create(Guid correlationId)
        {
            var instance = new T
            {
                MessageId = Guid.NewGuid(),
                CorrelationId = correlationId,
                Timestamp = DateTime.UtcNow,
                Stopwatch = Stopwatch.StartNew(),
            };
            return instance;
        }

        public static T CreateFrom(IServiceMessage caller)
        {

            var correlationId = caller.CorrelationId == Guid.Empty
                ? caller.MessageId
                : caller.CorrelationId;
            var message = Create(correlationId);
            return message;

        }

    }


}

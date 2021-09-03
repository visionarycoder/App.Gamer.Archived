namespace Util.ServiceMessaging
{
    public abstract class ServiceMessageResponse : ServiceMessageRequest, IServiceMessageResponse
    {

        public string Errors { get; set; }

    }

}
namespace Gamer.Utility.ServiceMessaging
{

    public abstract class ServiceMessageResponse : ServiceMessageRequest, IServiceMessageResponse
    {

        public string Errors { get; set; }
        public bool HasErrors => ! string.IsNullOrWhiteSpace(Errors);

    }

}
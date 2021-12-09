namespace Gamer.Utility.ServiceMessaging
{

    public interface IServiceMessageResponse : IServiceMessageRequest
    {

        string Errors { get; set; }
        bool HasErrors => !string.IsNullOrWhiteSpace(Errors);

    }

}
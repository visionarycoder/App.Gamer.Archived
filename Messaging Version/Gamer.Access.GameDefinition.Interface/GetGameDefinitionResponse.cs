using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.GameDefinition.Interface
{
    public class GetGameDefinitionResponse : ServiceMessageResponse
    {
        public GameDefinition GameDefinition { get; set; }

    }
}
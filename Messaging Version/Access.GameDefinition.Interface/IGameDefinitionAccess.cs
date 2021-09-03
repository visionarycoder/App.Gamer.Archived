using System.Threading.Tasks;
using Util.ServiceMessaging;

namespace Access.GameDefinition.Interface
{
    public interface IGameDefinitionAccess
    {
        Task<GetGameDefinitionResponse> GetGameDefinition(GetGameDefinitionRequest request);
    }

    public class GameDefinition
    {
        public int[] NumberOfPlayers { get; set; }
        public string Name { get; set; }
        public object[]
    }

    public class GetGameDefinitionRequest : ServiceMessageRequest
    {

    }

    public class GetGameDefinitionResponse : ServiceMessageResponse
    {
        public GameDefinition[] GameDefinitions { get; set; }
    }

}
using System.Threading.Tasks;


namespace Access.GameSession.Interface
{

    public interface IGameSessionAccess
    {
        Task<CreateGameSessionResponse> CreateGameSession(CreateGameSessionRequest request);
        Task<RetrieveGameSessionResponse> RetrieveGameSession(RetrieveGameSessionRequest request);
        Task<ApplyGameSessionChangeResponse> ApplyGameSessionChange(ApplyGameSessionChangesRequest request);
    }
}
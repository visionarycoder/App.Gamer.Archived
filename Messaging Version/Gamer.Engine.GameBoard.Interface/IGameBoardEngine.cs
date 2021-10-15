using System.Threading.Tasks;
using Gamer.Framework;

namespace Gamer.Engine.GameBoard.Interface
{
	public interface IGameBoardEngine : IServiceBase
	{
		Task<GetGameBoardResponse> GetBoardAsync(GetGameBoardRequest request);
	}
}
using System.Data;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Engine.GameBoard.Interface
{
	public class GetGameBoardResponse : ServiceMessageResponse
	{
		public DataTable GameBoard { get; set; }
	}
}
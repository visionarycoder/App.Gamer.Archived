using System.Data;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Engine.GameBoard.Interface
{
	public class GetGameBoardResponse : ServiceMessageResponse
	{
		public DataTable GameBoard { get; set; }
	}
}
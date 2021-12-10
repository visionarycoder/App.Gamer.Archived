using System.Data;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetBoardResponse : ServiceMessageResponse
	{
		public DataTable GameBoard { get; set; }
	}
}
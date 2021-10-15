using System.Data;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetBoardResponse : ServiceMessageResponse
	{
		public DataTable GameBoard { get; set; }
	}
}
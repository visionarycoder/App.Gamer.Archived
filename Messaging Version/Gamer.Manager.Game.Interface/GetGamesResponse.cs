using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetGamesResponse : ServiceMessageResponse
	{
		public GameDefinition[] GameDefinitions { get; set; }
	}
}
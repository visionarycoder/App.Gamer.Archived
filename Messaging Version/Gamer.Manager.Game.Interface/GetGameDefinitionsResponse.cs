using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetGameDefinitionsResponse : ServiceMessageResponse
	{
		public GameDefinition[] GameDefinitions { get; set; }
	}
}
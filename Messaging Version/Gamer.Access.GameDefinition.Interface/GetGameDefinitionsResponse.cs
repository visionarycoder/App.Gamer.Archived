using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.GameDefinition.Interface
{
	public class GetGameDefinitionsResponse : ServiceMessageResponse
	{
		public GameDefinition[] GameDefinitions { get; set; }
	}
}
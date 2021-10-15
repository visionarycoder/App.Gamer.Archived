using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.GameDefinition.Interface
{
	public class GetGameDefinitionsResponse : ServiceMessageResponse
	{
		public GameDefinition[] GameDefinitions { get; set; }
	}
}
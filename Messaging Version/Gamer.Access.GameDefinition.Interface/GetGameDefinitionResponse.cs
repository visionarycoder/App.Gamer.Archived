using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.GameDefinition.Interface
{
	public class GetGameDefinitionResponse : ServiceMessageResponse
	{
		public GameDefinition GameDefinition { get; set; }

	}
}
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class IsTileOpenResponse : ServiceMessageResponse
	{
		public bool Value { get; set; }
	}
}
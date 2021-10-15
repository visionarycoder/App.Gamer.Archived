using System.ComponentModel.DataAnnotations;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class ConfirmUsableAccessResponse : ServiceMessageResponse
	{
		public ValidationResult ValidationResult { get; set; }
	}
}
using Gamer.Utility.ServiceMessaging;

using System.ComponentModel.DataAnnotations;

namespace Gamer.Manager.Game.Interface
{
    public class ConfirmUsableAccessResponse : ServiceMessageResponse
    {
        public ValidationResult ValidationResult { get; set; }
    }
}
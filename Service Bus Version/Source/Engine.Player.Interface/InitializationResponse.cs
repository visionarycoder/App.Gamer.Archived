using System.Runtime.Serialization;
using Gamer.Framework;
using Gamer.Framework.Messaging;

namespace Gamer.Engine.Player.Interface
{

	[DataContract]
	public class InitializationResponse : MessageResponse
	{

		[DataMember]
		public InitializationState InitializationState { get; set; }

		[DataMember]
		public OperationStatus OperationStatus { get; set; }

	}

}

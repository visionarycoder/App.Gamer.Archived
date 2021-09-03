using System.Runtime.Serialization;
using System.ServiceModel;

namespace Gamer.Engine.Player.Interface
{

	[ServiceContract]
	public class PlayerDescription
	{

		[DataMember]
		public string PlayerName { get; set; }

		[DataMember]
		public string PlayerGamePiece { get; set; }

		[DataMember]
		public bool IsMachine { get; set; }

	}

}

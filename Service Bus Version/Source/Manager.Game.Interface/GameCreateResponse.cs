using System.Collections.Generic;
using System.Runtime.Serialization;
using Gamer.Framework.Messaging;

namespace Gamer.Manager.Game.Interface
{

	[DataContract]
	public class GameCreateResponse : MessageResponse
	{

		[DataMember]
		public KeyValuePair<string,string>[] GamePieces { get; set; } = { };
		
	}

}

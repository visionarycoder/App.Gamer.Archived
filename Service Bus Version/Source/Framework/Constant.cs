using System.Collections.Generic;

namespace Gamer.Framework
{

	public static class Constant
	{

		public const bool KEEP_ALIVE = true;

		public static class Messages
		{

			public const string NOT_YOUR_TURN = "Not your turn";
			public const string ERROR = "Error";

		}

		public static class EventOperation
		{

			public const string START_GAME_RESPONSE = "START_GAME_RESPONSE";
			public const string END_GAME_RESPONSE = "END_GAME_RESPONSE";
			public const string PLAY_TURN_RESPONSE = "PLAY_TURN_RESPONSE";

			public const string START_GAME_REQUEST = "START_GAME_REQUEST";
			public const string END_GAME_REQUEST = "END_GAME_REQUEST";
			public const string PLAY_TURN_REQUEST = "PLAY_TURN_REQUEST";

			public const string TRACE_EVENT = "TRACE_EVENT";

		}

		public static class TicTacToe
		{

			public const string DEFAULT_GAMEPIECE = " ";
			public static readonly IList<int> ALLOWED_NUMBER_OF_PLAYERS = new List<int> { 1, 2 };

		}

	}

}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.GameSession.Interface;
using Gamer.Access.Player.Interface;
using Gamer.Access.Tile.Interface;
using Gamer.Engine.GameBoard.Interface;
using Gamer.Framework;
using Gamer.Framework.ServiceMessaging;
using Microsoft.Extensions.Logging;

namespace Gamer.Engine.GameBoard.Service
{

	public class GameBoardEngine : ServiceBase, IGameBoardEngine
	{

		private readonly IGameSessionAccess gameSessionAccess;
		private readonly IPlayerAccess playerAccess;
		private readonly ITileAccess tileAccess;

		public GameBoardEngine(IGameSessionAccess gameSessionAccess, IPlayerAccess playerAccess, ITileAccess tileAccess, ILogger logger)
		:base(logger)
		{
			this.gameSessionAccess = gameSessionAccess;
			this.playerAccess = playerAccess;
			this.tileAccess = tileAccess;
		}

		public async Task<GetGameBoardResponse> GetBoardAsync(GetGameBoardRequest request)
		{

			var response = ServiceMessageFactory<GetGameBoardResponse>.CreateFrom(request);

			var gameSessionRequest = ServiceMessageFactory<GetGameSessionRequest>.CreateFrom(request);
			gameSessionRequest.GameSessionId = request.GameSessionId;
			var gameSessionResponse = await gameSessionAccess.GetGameSessionAsync(gameSessionRequest);
			if (gameSessionResponse.HasErrors)
			{
				response.Errors = "Unable to get the selected game board.  Session error.";
				return response;
			}

			var gameSession = gameSessionResponse.GameSession;

			// Board for Tic-Tac-Toe
			var players = new List<Player>();
			foreach (var playerId in gameSession.PlayerIds)
			{
				var playerRequest = ServiceMessageFactory<GetPlayerRequest>.CreateFrom(request);
				playerRequest.PlayerId = playerId;
				var playerResponse = await playerAccess.GetPlayerAsync(playerRequest);
				players.Add(playerResponse.Player);
			}

			response.GameBoard = new DataTable();
			response.GameBoard.Columns.Add(new DataColumn(" "));
			response.GameBoard.Columns.Add(new DataColumn("A"));
			response.GameBoard.Columns.Add(new DataColumn("B"));
			response.GameBoard.Columns.Add(new DataColumn("C"));

			var tileRequest = ServiceMessageFactory<FindTilesRequest>.CreateFrom(request);
			tileRequest.Filter = tile => tile.GameSessionId == request.GameSessionId;
			var tileResponse = await tileAccess.FindTilesAsync(tileRequest);
			var tiles = tileResponse.Tiles;
			if (tileResponse.HasErrors)
			{
				return response;
			}

			var dataRow = response.GameBoard.NewRow();
			dataRow[0] = 1;
			dataRow[1] = (tiles[0].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[0].PlayerId).GamePiece);
			dataRow[2] = (tiles[3].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[3].PlayerId).GamePiece);
			dataRow[3] = (tiles[6].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[6].PlayerId).GamePiece);
			response.GameBoard.Rows.Add(dataRow);

			dataRow = response.GameBoard.NewRow();
			dataRow[0] = 2;
			dataRow[1] = (tiles[1].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[1].PlayerId).GamePiece);
			dataRow[2] = (tiles[4].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[4].PlayerId).GamePiece);
			dataRow[3] = (tiles[7].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[7].PlayerId).GamePiece);
			response.GameBoard.Rows.Add(dataRow);

			dataRow = response.GameBoard.NewRow();
			dataRow[0] = 3;
			dataRow[1] = (tiles[2].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[2].PlayerId).GamePiece);
			dataRow[2] = (tiles[5].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[5].PlayerId).GamePiece);
			dataRow[3] = (tiles[8].PlayerId == Guid.Empty ? " " : players.First(i => i.Id == tiles[8].PlayerId).GamePiece);
			response.GameBoard.Rows.Add(dataRow);
			
			return response;

		}

	}

}
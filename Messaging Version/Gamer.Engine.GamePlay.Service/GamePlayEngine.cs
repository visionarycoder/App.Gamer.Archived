using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Gamer.Access.GameDefinition.Interface;
using Gamer.Access.GameSession.Interface;
using Gamer.Access.Player.Interface;
using Gamer.Access.Tile.Interface;
using Gamer.Engine.GamePlay.Interface;
using Gamer.Engine.GamePlay.Service.Factory;
using Gamer.Engine.GamePlay.Service.Helper;
using Gamer.Framework;
using Gamer.Framework.ServiceMessaging;

using Microsoft.Extensions.Logging;

using GameSession = Gamer.Engine.GamePlay.Interface.GameSession;
using Player = Gamer.Engine.GamePlay.Interface.Player;
using Tile = Gamer.Engine.GamePlay.Interface.Tile;

namespace Gamer.Engine.GamePlay.Service
{

	public class GamePlayEngine : ServiceBase, IGamePlayEngine
	{

		private readonly AutoPlayer autoPlayer = new AutoPlayer();

		private readonly IGameDefinitionAccess gameDefinitionAccess;
		private readonly IGameSessionAccess gameSessionAccess;
		private readonly IPlayerAccess playerAccess;
		private readonly ITileAccess tileAccess;

		public GamePlayEngine(IGameDefinitionAccess gameDefinitionAccess, IGameSessionAccess gameSessionAccess, IPlayerAccess playerAccess, ITileAccess tileAccess, ILogger logger)
		: base(logger)
		{

			this.gameDefinitionAccess = gameDefinitionAccess;
			this.gameSessionAccess = gameSessionAccess;
			this.playerAccess = playerAccess;
			this.tileAccess = tileAccess;

		}

		public async Task<IsGamePlayableResponse> IsGamePlayableAsync(IsGamePlayableRequest request)
		{

			var response = ServiceMessageFactory<IsGamePlayableResponse>.CreateFrom(request);

			var tilesRequest = ServiceMessageFactory<FindTilesRequest>.CreateFrom(request);
			tilesRequest.Filter = tile => tile.GameSessionId == request.GameSessionId;
			var tileResponse = await tileAccess.FindTilesAsync(tilesRequest);
			var tiles = tileResponse.Tiles;

			if (tiles.All(i => i.IsEmpty))
			// New Game
			{
				response.Value = true;
			}
			else if (tiles.All(i => !i.IsEmpty))
			// No empty spaces.
			{
				response.Value = false;
			}

			// Check all possible vectors
			var dictionary = tiles.ToDictionary(tile => tile.Address, tile => tile.Convert());

			// A Col
			if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
			{
				response.Value = false;
			}
			else

			// B Col
			if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
			{
				response.Value = false;
			}
			else

			// C Col
			if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
			{
				response.Value = false;
			}
			else

			// 1 Row
			if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
			{
				response.Value = false;
			}
			else

			// 2 Row
			if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
			{
				response.Value = false;
			}
			else

			// 3 Row
			if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
			{
				response.Value = false;
			}
			else

			// Right Diagonal
			if (IsWinningVector(dictionary["A1"], dictionary["B2"], dictionary["C3"]))
			{
				response.Value = false;
			}
			else

			// Left Diagonal
			if (IsWinningVector(dictionary["A3"], dictionary["B2"], dictionary["C1"]))
			{
				response.Value = false;
			}
			else
			{
				response.Value = true;
			}
			return response;

		}

		public async Task<FindWinnerResponse> FindWinnerAsync(FindWinnerRequest request)
		{

			var response = ServiceMessageFactory<FindWinnerResponse>.CreateFrom(request);

			var tilesRequest = ServiceMessageFactory<FindTilesRequest>.CreateFrom(request);
			tilesRequest.Filter = tile => tile.GameSessionId == request.GameSessionId;
			var tilesResponse = await tileAccess.FindTilesAsync(tilesRequest);
			var dictionary = tilesResponse.Tiles.ToDictionary(tile => tile.Address, tile => tile.Convert());
			var playerId = Guid.Empty;

			// Check all possible vectors
			// A Col
			if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
			{
				playerId = dictionary["A1"].PlayerId;
			}

			// B Col
			else if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
			{
				playerId = dictionary["B1"].PlayerId;
			}

			// C Col
			else if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
			{
				playerId = dictionary["C1"].PlayerId;
			}

			// 1 Row
			else if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
			{
				playerId = dictionary["A1"].PlayerId;
			}

			// 2 Row
			else if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
			{
				playerId = dictionary["B1"].PlayerId;
			}

			// 3 Row
			else if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
			{
				playerId = dictionary["C1"].PlayerId;
			}

			// Right Diagonal
			else if (IsWinningVector(dictionary["A1"], dictionary["B2"], dictionary["C3"]))
			{
				playerId = dictionary["A1"].PlayerId;
			}

			// Left Diagonal
			else if (IsWinningVector(dictionary["A3"], dictionary["B2"], dictionary["C1"]))
			{
				playerId = dictionary["A3"].PlayerId;
			}

			var playerRequest = ServiceMessageFactory<GetPlayerRequest>.CreateFrom(request);
			playerRequest.PlayerId = playerId;
			var playerResponse = await playerAccess.GetPlayerAsync(playerRequest);
			response.Player = playerResponse.Player.Convert();

			return response;

		}

		private static bool IsWinningVector(params Tile[] vectors)
		{

			var first = vectors.First().PlayerId;
			var results = vectors.All(i => i.PlayerId == first && i.PlayerId != Guid.Empty);
			return results;
		}

		public async Task<IsTileOpenResponse> IsTileOpenAsync(IsTileOpenRequest request)
		{

			var response = ServiceMessageFactory<IsTileOpenResponse>.CreateFrom(request);
			var tileRequest = ServiceMessageFactory<FindTilesRequest>.CreateFrom(request);
			tileRequest.Filter = tile => tile.GameSessionId == request.GameSessionId;
			var tileResponse = await tileAccess.FindTilesAsync(tileRequest);
			response.Value = tileResponse.Tiles.First(i => i.Address == request.Address).IsEmpty;
			return response;

		}

		public async Task<InitializeGameResponse> InitializeGameAsync(InitializeGameRequest request)
		{

			var response = ServiceMessageFactory<InitializeGameResponse>.CreateFrom(request);
			var gameDefinitionRequest = ServiceMessageFactory<GetGameDefinitionRequest>.CreateFrom(request);
			gameDefinitionRequest.GameDefinitionId = request.GameDefinitionId;

			var gameDefinitionResponse = await gameDefinitionAccess.GetGameDefinitionAsync(gameDefinitionRequest);

			// Create Players
			var players = new List<Player>();
			for (var idx = 0; idx < gameDefinitionResponse.GameDefinition.GamePieces.Length; idx++)
			{
				var name = $"Player {idx + 1}";
				var gamePiece = gameDefinitionResponse.GameDefinition.GamePieces[idx];
				var isMachine = request.NumberOfPlayers <= idx;

				var player = PlayerFactory.Create(name, gamePiece, isMachine);
				players.Add(player);
			}

			var createPlayersRequest = ServiceMessageFactory<CreatePlayersRequest>.CreateFrom(request);
			createPlayersRequest.Players = players.Convert();
			var createPlayerResponse = await playerAccess.CreatePlayersAsync(createPlayersRequest);
			if (createPlayerResponse.HasErrors)
			{
				logger.LogError($"{createPlayerResponse.Errors}");
			}


			var playerIds = players.Select(i => i.Id).ToArray();
			var gameSession = GameSessionFactory.Create(request.GameDefinitionId, playerIds);

			var gameSessionRequest = ServiceMessageFactory<CreateGameSessionRequest>.CreateFrom(request);
			gameSessionRequest.GameSession = gameSession.Convert();

			var gameSessionResponse = await gameSessionAccess.CreateGameSessionAsync(gameSessionRequest);
			if (! gameSessionResponse.HasErrors)
			{
				response.GameSession = gameSessionResponse.GameSession.Convert();
			}
			else
			{
				response.Errors += "Unable to create game session.";
				logger.LogError($"Unable to create game session ({response.GameSession.Id}).");
			}

			var tilesRequest = ServiceMessageFactory<CreateTilesRequest>.CreateFrom(request);
			var tiles = TicTacToeBoardFactory.Create(gameSession.Id, new[] {"A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3"});
			tilesRequest.Tiles = tiles.Convert();

			var tilesResponse = await tileAccess.CreateTilesAsync(tilesRequest);
			if (! tilesResponse.HasErrors)
			{
				return response;
			}

			response.Errors += "Unable to create game tiles.";
			logger.LogError($"Unable to create game tiles for game session ({response.GameSession.Id}).");

			return response;

		}

		//public async Task<EmptyResponse> IncrementPlayer(IncrementPlayerRequest request)
		//{

		//	var response = ServiceMessageFactory<EmptyResponse>.CreateFrom(request);
		//	var gameSessionRequest = ServiceMessageFactory<GetGameSessionRequest>.CreateFrom(request);
		//	gameSessionRequest.GameSessionId = request.gameSessionId;

		//	var gameSessionResponse = await gameSessionAccess.GetGameSessionAsync(gameSessionRequest);
		//	if (gameSessionResponse.HasErrors)
		//	{
		//		response.Errors += "Unable to get the game session.";
		//		return response;
		//	}

		//	IncrementPlayer(gameSessionResponse.GameSession.Convert());
		//	var updateGameSessionRequest = ServiceMessageFactory<UpdateGameSessionRequest>.CreateFrom(request);
		//	updateGameSessionRequest.GameSession = gameSessionResponse.GameSession;
		//	var updateGameSessionResponse = await gameSessionAccess.UpdateGameSessionAsync(updateGameSessionRequest);
		//	if (updateGameSessionResponse.HasErrors)
		//	{
		//		response.Errors += "Unable to update the game session";
		//	}
		//	return response;

		//}

		private void IncrementPlayer(GameSession gameSession)
		{

			var playerIds = gameSession.PlayerIds.ToList();
			var idx = playerIds.IndexOf(gameSession.CurrentPlayerId) + 1;
			var nextPlayerId = idx < playerIds.Count()
				? playerIds[idx]
				: playerIds[0];
			gameSession.CurrentPlayerId = nextPlayerId;

		}

		public async Task<PlayTurnResponse> PlayTurnAsync(PlayTurnRequest request)
		{

			var response = ServiceMessageFactory<PlayTurnResponse>.CreateFrom(request);
			var playerRequest = ServiceMessageFactory<GetPlayerRequest>.CreateFrom(request);
			playerRequest.PlayerId = request.PlayerId;
			var playerResponse = await playerAccess.GetPlayerAsync(playerRequest);
			var player = playerResponse.Player.Convert();

			var gameSessionRequest = ServiceMessageFactory<GetGameSessionRequest>.CreateFrom(request);
			gameSessionRequest.GameSessionId = request.GameSessionId;
			var gameSessionResponse = await gameSessionAccess.GetGameSessionAsync(gameSessionRequest);
			var gameSession = gameSessionResponse.GameSession.Convert();

			var tilesRequest = ServiceMessageFactory<FindTilesRequest>.CreateFrom(request);
			tilesRequest.Filter = tile => tile.GameSessionId == request.GameSessionId;
			var tilesResponse = await tileAccess.FindTilesAsync(tilesRequest);
			var tiles = tilesResponse.Tiles;
			Access.Tile.Interface.Tile tile;

			if (player.IsMachine)
			{
				tile = autoPlayer.PlayTurn(tiles);
				tile.PlayerId = player.Id;
			}
			else
			{
				var address = request.Address.ToUpperInvariant();
				tile = tiles.First(i => i.Address == address);
				tile.PlayerId = player.Id;
			}

			var updateTileRequest = ServiceMessageFactory<UpdateTileRequest>.CreateFrom(request);
			updateTileRequest.Tile = tile;
			var updateTileResponse = await tileAccess.UpdateTileAsync(updateTileRequest);
			if (updateTileResponse.HasErrors)
			{
				response.Errors += "Tile update error.";
			}
			IncrementPlayer(gameSession);

			return response;

		}

	}

}
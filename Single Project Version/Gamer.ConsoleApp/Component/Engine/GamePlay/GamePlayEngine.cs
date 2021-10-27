using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Component.Access.GameDefinition;
using Gamer.Component.Access.GameSession;
using Gamer.Component.Access.Player;
using Gamer.Component.Access.Tile;
using Gamer.Component.Engine.GameBoard;

namespace Gamer.Component.Engine.GamePlay
{

	public class GamePlayEngine : IGamePlayEngine
	{

		private readonly AutoPlayer autoPlayer = new AutoPlayer();

		private readonly IGameDefinitionAccess gameDefinitionAccess;
		private readonly IGameSessionAccess gameSessionAccess;
		private readonly IPlayerAccess playerAccess;
		private readonly ITileAccess tileAccess;

		public GamePlayEngine(IGameDefinitionAccess gameDefinitionAccess, IGameSessionAccess gameSessionAccess, IPlayerAccess playerAccess, ITileAccess tileAccess)
		{
			
			this.gameDefinitionAccess = gameDefinitionAccess;
			this.gameSessionAccess = gameSessionAccess;
			this.playerAccess = playerAccess;
			this.tileAccess = tileAccess;

		}

		public async Task<bool> IsPlayable(Guid gameSessionId)
		{

			var tiles = await tileAccess.FindTiles(gameSessionId);
			if (tiles.All(i => i.IsEmpty))
				return true;  // New Game

			if (tiles.All(i => !i.IsEmpty))
				return false;  // No empty spaces.

			// Check all possible vectors
			var dictionary = tiles.ToDictionary(tile => tile.Address, tile => tile);

			// A Col
			if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
				return false;

			// B Col
			if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
				return false;

			// C Col
			if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
				return false;

			// 1 Row
			if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
				return false;

			// 2 Row
			if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
				return false;

			// 3 Row
			if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
				return false;

			// Right Diagonal
			if (IsWinningVector(dictionary["A1"], dictionary["B2"], dictionary["C3"]))
				return false;

			// Left Diagonal
			if (IsWinningVector(dictionary["A3"], dictionary["B2"], dictionary["C1"]))
				return false;

			return true;

		}

		public async Task<Player> FindWinner(Guid gameSessionId)
		{

			if (await IsPlayable(gameSessionId))
			{
				throw new ApplicationException("Game is still playable.");
			}

			var tiles = await tileAccess.FindTiles(gameSessionId);
			var dictionary = tiles.ToDictionary(tile => tile.Address, tile => tile);
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

			var players = await playerAccess.FindPlayers(i => i.Id == playerId);
			var player = players.FirstOrDefault();
			return player;

		}

		private static bool IsWinningVector(params Tile[] vectors)
		{

			var first = vectors.First().PlayerId;
			var results = vectors.All(i => i.PlayerId == first && i.PlayerId != Guid.Empty);
			return results;
		}

		public async Task<bool> IsTileOpen(Guid gameSessionId, string address)
		{

			var tiles = await tileAccess.FindTiles(gameSessionId);
			return tiles.First(i => i.Address == address).IsEmpty;

		}

		public async Task<GameSession> InitializeGame(Guid gameDefinitionId, int numberOfPlayers)
		{

			var gameDefinitions = await gameDefinitionAccess.FindGameDefinitions(i => i.Id == gameDefinitionId);
			var gameDefinition = gameDefinitions.FirstOrDefault();
			var createdPlayers = await CreatePlayers(numberOfPlayers, gameDefinition);
			var gameSession = GameSessionFactory.Create(gameDefinitionId, createdPlayers.Select(i => i.Id).ToArray());
			await gameSessionAccess.ProvisionGameSession(gameSession);
			var tiles = TicTacToeBoardFactory.Create(gameSession.Id, new[] { "A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3" });
			await tileAccess.ProvisionTiles(tiles.ToArray());
			return gameSession;

		}

		private async Task<Player[]> CreatePlayers(int numberOfPlayers, GameDefinition gameDefinition)
		{
			// Create Players
			var players = new List<Player>();
			for (var idx = 0; idx < gameDefinition.GamePieces.Length; idx++)
			{
				var name = $"Player {idx + 1}";
				var gamePiece = gameDefinition.GamePieces[idx];
				var isMachine = numberOfPlayers <= idx;

				var player = PlayerFactory.Create(name, gamePiece, isMachine);
				players.Add(player);
			}
			
			var createdPlayers = await playerAccess.ProvisionPlayers(players.ToArray());
			return createdPlayers;
		}

		public async Task<bool> IncrementPlayer(Guid gameSessionId)
		{
			
			var gameSessions = await gameSessionAccess.FindGameSession(i => i.Id == gameSessionId);
			var gameSession = gameSessions.FirstOrDefault();
			var result = await IncrementPlayer(gameSession);
			return result;

		}

		public async Task<bool> IncrementPlayer(GameSession gameSession)
		{

			var playerIds = gameSession.PlayerIds.ToList();
			var idx = playerIds.IndexOf(gameSession.CurrentPlayerId) + 1;
			var nextPlayerId = idx < playerIds.Count()
				? playerIds[idx]
				: playerIds[0];
			gameSession.CurrentPlayerId = nextPlayerId;
			var result = await gameSessionAccess.UpdateGameSession(gameSession);
			return result != null;

		}

		public async Task<bool> AutoPlayTurn(Guid gameSessionId)
		{

			var gameSessions = await gameSessionAccess.FindGameSession(i => i.Id == gameSessionId);
			var gameSession = gameSessions.FirstOrDefault();
			var currentPlayers = await playerAccess.FindPlayers(i => i.Id == gameSession.CurrentPlayerId);
			var currentPlayer = currentPlayers.FirstOrDefault();
			var tiles = await tileAccess.FindTiles(gameSessionId);
			var tile = autoPlayer.PlayTurn(tiles);
			tile.PlayerId = currentPlayer.Id;
			var result = await tileAccess.UpdateTile(tile);
			result =  result && await IncrementPlayer(gameSessionId);
			return result;

		}

		public async Task<bool> PlayTurn(Guid gameSessionId, Guid playerId, string address)
		{

			// This should throw an error
			if (string.IsNullOrWhiteSpace(address))
				return false;

			address = address.ToUpperInvariant();
			var tiles = await tileAccess.FindTiles(gameSessionId);
			var tile = tiles.FirstOrDefault(i => i.Address == address);
			if (tile == null)
			{
				Trace.WriteLine("Error!");
				return false;
			}
			tile.PlayerId = playerId;
			var result = await tileAccess.UpdateTile(tile);
			result = result && await IncrementPlayer(gameSessionId);
			return result;


		}

		public async Task<bool> EndGame(Guid gameSessionId)
		{
			var gameSessions = await gameSessionAccess.FindGameSession(i => i.Id == gameSessionId);
			var gameSession = gameSessions.FirstOrDefault();
			var result = await gameSessionAccess.RemoveGameSession(gameSession);
			return result;
		}

	}

}
using System;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.Player.Interface;
using Gamer.Engine.Board.Interface;
using Gamer.Engine.Board.Service;
using Gamer.Engine.Game.Interface;
using Gamer.Engine.Game.Service;
using Gamer.Engine.Player.Interface;
using Gamer.Engine.Player.Service;
using Gamer.Framework;
using Gamer.Manager.Game.Interface;
using ServiceModelEx;

namespace Gamer.Manager.Game.Service
{

	public class GameManger : IGameManger
	{

		public async Task<GameCreateResponse> StartGame(GameCreateRequest gameDefintion)
		{

			var playerEngine = InProcFactory.CreateInstance<PlayerEngine, IPlayerEngine>();
			var boardEngine = InProcFactory.CreateInstance<BoardEngine, IBoardEngine>();
			var gameEngine = InProcFactory.CreateInstance<GameEngine, IGameEngine>();

			var gameId = Guid.NewGuid();
			var wasSuccessful = await playerEngine.InitializePlayers(gameId, gameDefintion.NumberOfPlayers);
			if (!wasSuccessful)
				throw new ApplicationException("Unable to initialize players.");

			var players = await playerEngine.GetPlayers(gameId);
			var playerIds = players.ToList().Select(i => i.PlayerId).ToArray();
			var board = await boardEngine.CreateBoard(gameId);
			if(board == null)
				throw new ApplicationException("Unable to initialize board.");

			wasSuccessful = await gameEngine.CreateGame(gameId, playerIds);
			if(! wasSuccessful)
				throw new ApplicationException("Unable to initialize game.");

			var response = new GameCreateResponse
			{
				CorrelationId = gameDefintion.CorrelationId,
				GamePieces = board.AddressGamePieceMap,
				Message = "Ready",
			};

			return response;

		}


		public async Task<TurnResponse> PlayTurn(TurnRequest turnRequest)
		{

			var result = HandleTurn(turnRequest);
			return await result;

		}

		public async Task<bool> EndGame(Guid gameId)
		{
			
			var playerEngine = InProcFactory.CreateInstance<PlayerEngine, IPlayerEngine>();
			var boardEngine = InProcFactory.CreateInstance<BoardEngine, IBoardEngine>();
			var gameEngine = InProcFactory.CreateInstance<GameEngine, IGameEngine>();

			var wasSuccessfull = await playerEngine.TearDownPlayers(gameId);
			if (!wasSuccessfull)
				throw new ApplicationException("Unable to delete players.");

			wasSuccessfull = await boardEngine.DestroyBoard(gameId);
			if (!wasSuccessfull)
				throw new ApplicationException("Unable to delete board.");

			wasSuccessfull = await gameEngine.DestroyGame(gameId);
			if (!wasSuccessfull)
				throw new ApplicationException("Unable to delete game.");

			return true;

		}

		private async Task<TurnResponse> HandleTurn(TurnRequest turnRequest)
		{

			var gameEngine = InProcFactory.CreateInstance<GameEngine, IGameEngine>();

			var turnResult = new TurnResponse
			{
				TurnRequest = turnRequest,
			};

			var filter = new PlayerFilter
			{
				GameId = turnRequest.SessionId,
				CurrentPlayer = true,
			};

			var isCurrentPlayer = await gameEngine.IsCurrentPlayer(turnRequest.PlayerId);
			if (!isCurrentPlayer)
			{
				turnResult.OperationStatus = OperationStatus.Completed;
				turnResult.HasError = true;
				turnResult.ErrorMessage = Constant.Messages.NOT_YOUR_TURN;
				return turnResult;
			}

			var boardEngine = InProcFactory.CreateInstance<BoardEngine, IBoardEngine>();
			var isPlayable = await boardEngine.IsGamePlayable(turnRequest.SessionId);
			if (!isPlayable)
			{
				turnResult.OperationStatus = OperationStatus.Completed;
				turnResult.Message = "Board is not playable";
				return turnResult;
			}

			var isPlayablePosition = await boardEngine.IsPositionAvailable(turnRequest.SessionId, turnRequest.Column, turnRequest.Row);
			if (!isPlayablePosition)
			{
				turnResult.OperationStatus = OperationStatus.Completed;
				turnResult.Message = "Space is not playable.";
				return turnResult;
			}

			var playerEngine = InProcFactory.CreateInstance<PlayerEngine, IPlayerEngine>();
			var player = await playerEngine.GetPlayer(turnRequest.PlayerId);
			var result = await boardEngine.PlaceTile(turnRequest.SessionId, turnRequest.Column, turnRequest.Row, player.GamePiece);

			turnResult.OperationStatus = OperationStatus.Completed;
			turnResult.ErrorMessage = result
				? ""
				: "Unable to play that position";

			return turnResult;

		}

	}

}
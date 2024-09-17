using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Gamer.Client.ConsoleApp.Data;

using Gamer.Client.ConsoleApp.Framework;

namespace Gamer.Client.ConsoleApp.Access.Game
{

    public interface IGameDefinitionAccess
    {
        Task<List<GameDefinition>> GetGameDefinitions();
        Task<GameDefinition> GetGameDefinition(Guid gameDefinitionId);
        Task<List<GameDefinition>> FindGameDefinitions(GameDefinitionFilter filter);
    }


    internal class GameAccess
    {
    }

    [DataContract]
    public class GameDefinition
    {

        [DataMember] public Guid Id { get; init; }
        [DataMember] public string Name { get; init; } = string.Empty;
        [DataMember] public string Description { get; init; } = string.Empty;
        [DataMember] public List<Guid> GamePieceIds { get; set; } = new List<Guid>();
        [DataMember] public string TurnPrompt { get; init; } = string.Empty;
        [DataMember] public int MaxNumberOfPlayers { get; init; }
        [DataMember] public int MinNumberOfPlayers { get; init; }

    }

    public class GameDefinitionFilter
    {
        public Guid GameDefinitionId { get; set; }
    }

    internal class GameDefinitionAccess : Service<GameDefinitionAccess>, IGameDefinitionAccess
    {

        private readonly GamerDbContext db;

        public GameDefinitionAccess(GamerDbContext context, ILogger<GameDefinitionAccess> logger)
            : base(logger)
        {
            db = context;
        }

        public async Task<List<Interface.GameDefinition>> GetGameDefinitions()
        {
            throw new NotImplementedException();
        }

        public async Task<Interface.GameDefinition> GetGameDefinition(Guid gameDefinitionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Interface.GameDefinition>> FindGameDefinitions(GameDefinitionFilter filter)
        {
            var dbObject = await db.GameDefinitions.SingleOrDefaultAsync(i => i.Id == filter.GameDefinitionId);
            return dbObject.Convert();
        }
    }

    [DataContract]
    public class GameSession
    {

        [DataMember] public Guid Id { get; init; }
        [DataMember] public List<Guid> PlayerIds { get; init; }
        [DataMember] public Guid GameDefinitionId { get; init; }
        [DataMember] public Guid CurrentPlayerId { get; set; }

    }

    internal class GamePieceAccess : Service<GamePieceAccess>, IGamePieceAccess
    {

        private GamerDbContext db;

        public GamePieceAccess(GamerDbContext dbContext, ILogger logger)
            : base(logger)
        {
            db = dbContext;
        }

        public async Task<List<Interface.GamePiece>> CreateGamePiecesAsync(List<Interface.GamePiece> gamePieces)
        {
            var dbObject = gamePieces.Select(i => i.Convert());
            await db.GamePieces.AddRangeAsync(dbObject);
            throw new NotImplementedException();
        }

        public async Task<List<Interface.GamePiece>> FindGamePiecesAsync(Func<Interface.GamePiece, bool> filter)
        {
            Contract.Assert(filter != null);
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveGamePieceAsync(Guid gameSessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemovesGamePieceAsync(List<Guid> gameSessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Interface.GamePiece>> UpdateGamePiecesAsync(List<Interface.GamePiece> gamePieces)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Interface.GamePiece>> UpdateGamePieceAsync(Interface.GamePiece gamePiece)
        {
            throw new NotImplementedException();
        }

    }

    internal static class AddressEx
    {
        public static Resource.DataSource.Model.Address Convert(this Interface.Address source)
        {
            var target = new Address()
            {

            };
            return target;
        }

        public static Interface.Address Convert(this Resource.DataSource.Model.Address source)
        {
            var target = new Interface.Address
            {

            };
            return target;
        }
    }

    internal static class GamePieceEx
    {
        public static Resource.DataSource.Model.GamePiece Convert(this Interface.GamePiece source)
        {
            var target = new Resource.DataSource.Model.GamePiece
            {
                Id = source.Id,
                Address = source.Address.Convert(),
                GameSessionId = source.GameSessionId,
                PlayerId = source.PlayerId
            };
            return target;
        }

        public static Interface.GamePiece Convert(this Resource.DataSource.Model.GamePiece source)
        {
            var target = new Interface.GamePiece
            {
                Id = source.Id,
                Address = source.Address.Convert(),
                GameSessionId = source.GameSessionId,
                PlayerId = source.PlayerId
            };
            return target;
        }
    }

    [DataContract]
    public class Address
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public int X { get; set; }
        [DataMember] public int Y { get; set; }
    }

    [DataContract]
    public class GamePiece
    {

        [DataMember] public Guid Id { get; set; }
        [DataMember] public Guid GameSessionId { get; set; }
        [DataMember] public Address Address { get; set; }
        [DataMember] public Guid PlayerId { get; set; }

    }

    public interface IGamePieceAccess
    {

        Task<List<GamePiece>> CreateGamePiecesAsync(List<GamePiece> gamePieces);
        Task<List<GamePiece>> FindGamePiecesAsync(Func<GamePiece, bool> filter);
        Task<bool> RemoveGamePieceAsync(Guid gameSessionId);
        Task<bool> RemovesGamePieceAsync(List<Guid> gameSessionId);
        Task<List<GamePiece>> UpdateGamePiecesAsync(List<GamePiece> gamePieces);
        Task<List<GamePiece>> UpdateGamePieceAsync(GamePiece gamePiece);

    }

    internal static class GameDefinitionEx
    {

        public static Resource.DataSource.Model.GameDefinition Convert(this Interface.GameDefinition source)
        {
            var target = new Resource.DataSource.Model.GameDefinition
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                MaxNumberOfPlayers = source.MaxNumberOfPlayers,
                MinNumberOfPlayers = source.MinNumberOfPlayers,
                TurnPrompt = source.TurnPrompt,
                GamePieceIds = source.GamePieceIds,
            }

        }
    }

    internal class GameDefinitionAccess : Service<GameDefinitionAccess>, IGameDefinitionAccess
    {

        private readonly GamerDbContext db;

        public GameDefinitionAccess(GamerDbContext context, ILogger<GameDefinitionAccess> logger)
            : base(logger)
        {
            db = context;
        }

        public async Task<List<Interface.GameDefinition>> GetGameDefinitions()
        {
            throw new NotImplementedException();
        }

        public async Task<Interface.GameDefinition> GetGameDefinition(Guid gameDefinitionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Interface.GameDefinition>> FindGameDefinitions(GameDefinitionFilter filter)
        {
            var dbObject = await db.GameDefinitions.SingleOrDefaultAsync(i => i.Id == filter.GameDefinitionId);
            return dbObject.Convert();
        }

    }
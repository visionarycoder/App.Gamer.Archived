using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Gamer.Client.ConsoleApp.Framework;

namespace Gamer.Client.ConsoleApp.Access.Player
{
    internal static class PlayerEx
    {

        public static Interface.Player Convert(this VisionaryCoder.Resource.DataSource.Model.Player source)
        {
            var target = new Interface.Player
            {
                Id = source.Id,
                IsMachine = source.IsMachine,
                Name = source.Name,
                GamePieceIds = source.GamePieceIds,
            };
            return target;
        }

        public static VisionaryCoder.Resource.DataSource.Model.Player Convert(this Interface.Player source)
        {
            var target = new VisionaryCoder.Resource.DataSource.Model.Player
            {
                Id = source.Id,
                IsMachine = source.IsMachine,
                Name = source.Name,
                GamePieceIds = source.GamePieceIds,
            };
            return target;
        }

        public static string LabelValue(this object source, string propertyName)
        {
            var value = source.GetType().GetProperty(propertyName)?.GetValue(source)?.ToString();
            return $"\"{propertyName}\":\"{value}\"";
        }

        public static string ToJson(this object source)
        {

            var output = new StringBuilder("{");
            var propertyNames = source.GetType().GetProperties(BindingFlags.Public).Select(i => i.Name);
            foreach (var propertyName in propertyNames)
            {
                output.Append(source.LabelValue(propertyName));

            }
            output.AppendLine("}");
            return $"{output}";
        }

    }

    [DataContract]
    public class Player
    {
        [DataMember] public Guid Id { get; init; }
        [DataMember] public string Name { get; init; }
        [DataMember] public bool IsMachine { get; init; }
        [DataMember] public List<Guid> GamePieceIds { get; set; }
    }

    public class FindPlayersFilter
    {
        public List<Guid> PlayerIds { get; set; } = new List<Guid>();
    }

    public interface IPlayerAccess : IService
    {

        Task<Player> GetPlayer(Guid playerId);
        Task<List<Player>> FindPlayers(FindPlayersFilter filter);
        Task<Player> CreatePlayer(Player player);
        Task<List<Player>> CreatePlayers(List<Player> players);
        Task<bool> RemovePlayer(Player player);
        Task<bool> RemovePlayers(List<Player> players);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.GameDefinition.Service
{
    
    public static class GameDefinitionInitializer
    {

        public static Interface.GameDefinition[] Intitalize()
        {
            var collection = new List<Interface.GameDefinition>();
            var gameDefinition = new Interface.GameDefinition();
            gameDefinition.Name = "Tic-Tac-Toe";
            gameDefinition.NumberOfPlayers = new[] {0, 1, 2};
            collection.Add(gameDefinition);
            return .collection.ToArray();
        }
    }

}

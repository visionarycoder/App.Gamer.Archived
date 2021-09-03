using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Access.GameDefinition.Interface;

namespace Access.GameDefinition.Service
{

    public class GameDefinitionAccess : IGameDefinitionAccess
    {

        private static readonly HashSet<Interface.GameDefinition> cache;

        static GameDefinitionAccess()
        {
            var collection = GameDefinitionInitializer.Intitalize();
            cache = new HashSet<Interface.GameDefinition>(collection);
        }

        
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Access.Board.Interface;
using Util.Caching;

namespace Access.Board.Service
{
    
    public class BoardAccess : IBoardAccess
    {

        private static Cache<Board> cache = new Cache<Board>();

        public IGameTile[] GetGameBoard(Guid boardId)
        {
            
        }

        public bool UpdateGameBoard(IGameTile[] tiles)
        {
            
        }

    }

    public class GetGameBoardResponse
    {

    }
}
  

using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Access.Tile;

namespace TicTacToe.Engine.Board
{

    public class BoardEngine
    {

        private readonly List<Tile> cells = new List<Tile>();

        public IEnumerable<Tile> Cells => cells;

        public Tile A1 => cells[0];
        public Tile A2 => cells[1];
        public Tile A3 => cells[2];
        public Tile B1 => cells[3];
        public Tile B2 => cells[4];
        public Tile B3 => cells[5];
        public Tile C1 => cells[6];
        public Tile C2 => cells[7];
        public Tile C3 => cells[8];

        public bool IsEmpty => cells.All(i => i.IsEmpty);

        public bool IsFull => cells.All(i => !i.IsEmpty);

        public BoardEngine()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            cells.Clear();
            cells.AddRange(new List<Tile>
            {
                new Tile("A1"),
                new Tile("A2"),
                new Tile("A3"),
                new Tile("B1"),
                new Tile("B2"),
                new Tile("B3"),
                new Tile("C1"),
                new Tile("C2"),
                new Tile("C3"),
            });
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine($"  A" + " B" + " C");
            str.AppendLine($"1 {A1.GamePiece} {B1.GamePiece} {C1.GamePiece}");
            str.AppendLine($"2 {A2.GamePiece} {B2.GamePiece} {C2.GamePiece}");
            str.AppendLine($"3 {A3.GamePiece} {B3.GamePiece} {C3.GamePiece}");
            return str.ToString();
        }

    }

}
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Models
{

    public class Board
    {

        private readonly List<Cell> cells = new List<Cell>();

        public IEnumerable<Cell> Cells => cells;

        public Cell A1 => cells[0];
        public Cell A2 => cells[1];
        public Cell A3 => cells[2];
        public Cell B1 => cells[3];
        public Cell B2 => cells[4];
        public Cell B3 => cells[5];
        public Cell C1 => cells[6];
        public Cell C2 => cells[7];
        public Cell C3 => cells[8];

        public bool IsEmpty => cells.All(i => i.IsEmpty);

        public bool IsFull => cells.All(i => !i.IsEmpty);

        public Board()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            cells.Clear();
            cells.AddRange(new List<Cell>
            {
                new Cell("A1"),
                new Cell("A2"),
                new Cell("A3"),
                new Cell("B1"),
                new Cell("B2"),
                new Cell("B3"),
                new Cell("C1"),
                new Cell("C2"),
                new Cell("C3"),
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
namespace TicTacToe.Models
{

	public class Cell
	{

		public string Address { get; set; }
		public string GamePiece { get; set; } = " ";
		public bool IsEmpty => string.IsNullOrWhiteSpace(GamePiece);

		public Cell(string address)
		{
			Address = address;
		}

	}

}
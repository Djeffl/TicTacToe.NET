namespace TicTacToe.Client.AI.Models
{
	public class CellPosition
	{

		public CellPosition(int row, int column)
		{
			Row = row;
			Column = column;
		}
		public int Column { get; set; }

		public int Row { get; set; }
	}
}
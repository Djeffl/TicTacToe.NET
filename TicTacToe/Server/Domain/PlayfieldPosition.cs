namespace TicTacToe.Server.Domain
{
	public class PlayfieldPosition
	{
		public PlayfieldPosition(int row, int column)
		{
			Row = row;
			Column = column;
		}
		public int Column { get; set; }

		public int Row { get; set; }
	}
}

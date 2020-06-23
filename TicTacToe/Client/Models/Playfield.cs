using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Client.AI;
using TicTacToe.Client.Exceptions;
using TicTacToe.Client.Extensions;

namespace TicTacToe.Client.Models
{
	public class Playfield
	{
		private Random random = new Random();

		private string[,] FieldArray { get; set; }

		public Players Players { get; }

		public event EventHandler EndConditionReached;

		public bool IsFinished { get; private set; }

		public EndState? EndState { get; set; }

		public Player ActivePlayer
		{
			get
			{
				return Players.GetActivePlayer();
			}
		}

		public int ColumnSize { get; }

		public int RowSize { get; }

		public Engine Engine { get; set; }


		public Playfield(Players players, int rowSize, int columnSize)
		{
			Engine = new Engine();
			
			Players = players;

			RowSize = rowSize;
			ColumnSize = columnSize;

			FieldArray = new string[rowSize, columnSize];
		}

		public string GetValue(int row, int column)
		{
			return FieldArray[row, column];
		}

		private bool ValidateMove(int row, int column)
		{
			Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(FieldArray));
			Console.WriteLine($"row: {row} column: {column}");
			return string.IsNullOrEmpty(FieldArray[row, column]);
		}

		public void Move(int row, int column)
		{
			if (ValidateMove(row, column))
			{
				var mark = Players.GetCurrentPlayerMark();
				FieldArray[row, column] = mark.ToString();
				Players.SetNextActivePlayer();

				// Todo: extract to seperate method
				// Todo: return proper EventArgs with condition, last player, winner, time...
				if (CheckIfFinished(mark))
				{
					return;
				}	

				if (ActivePlayer.Type == PlayerType.Ai)
				{
					var move = Engine.Move(FieldArray);
					Move(move.Row, move.Column);
				}

				if (CheckIfFinished(mark))
				{
					return;
				}
			}

			throw new InvalidMoveException();
		}

		public bool CheckIfFinished(TicTacToeMark mark)
		{
			if (CheckIsWon())
			{
				var winnerIndex = Players.FindIndex(x => x.FieldMark == mark);
				if (winnerIndex == 0)
				{
					OnEndContionReached(new EndGameEventArgs(Models.EndState.Player1Won));
					return true;
				}
				else
				{
					OnEndContionReached(new EndGameEventArgs(Models.EndState.Player2Won));
					return true;
				}
			}
			else if (CheckIsTie())
			{
				OnEndContionReached(new EndGameEventArgs(Models.EndState.Tie));
				return true;
			}

			return false;
		}

		public void Start()
		{
			SetRandomPlayerActive();
			Console.WriteLine("Active start player type: " + ActivePlayer.Type);

			if (ActivePlayer.Type == PlayerType.Ai)
			{
				Console.WriteLine(JsonConvert.SerializeObject(FieldArray));
				var move = Engine.Move(FieldArray);
				Move(move.Row, move.Column);
			}
		}

		private bool CheckIsTie()
		{
			for (var row = 0; FieldArray.GetLength(0) > row; row++)
			{
				for (int column = 0; column < FieldArray.GetLength(1); column++)
				{
					if (string.IsNullOrEmpty(FieldArray[row, column]))
					{
						return false;
					}
				}
			}

			return true;
		}

		// Todo: can be extracted in a seperate interface
		private bool CheckIsWon()
		{
			// Winning conditions
			// X X X // X - - // X - -
			// - - - // - X - // X - -
			// - - - // - - X // X - -

			// Check if vertical winner
			for (var row = 0; FieldArray.GetLength(0) > row; row++)
			{
				var fullRow = new List<string>();
				for (int column = 0; column < FieldArray.GetLength(1); column++)
				{
					fullRow.Add(FieldArray[row, column]);
				}

				if (fullRow.IsWonTicTacToe())
				{
					return true;
				}
			}

			// Check if horizontal winner
			for (var column = 0; FieldArray.GetLength(1) > column; column++)
			{
				var fullRow = new List<string>();
				for (int row = 0; row < FieldArray.GetLength(0); row++)
				{
					fullRow.Add(FieldArray[row, column]);
				}

				if (fullRow.IsWonTicTacToe())
				{
					return true;
				}
			}

			// Check if diagonal winner
			for (int row = 0; row < FieldArray.GetLength(0); row++)
			{
				List<string> fullRow;
				for (int column = 0; column < FieldArray.GetLength(1); column++)
				{
					fullRow = new List<string>();

					fullRow.Add(GetCellValue(row - 1, column - 1));
					fullRow.Add(FieldArray[row, column]);
					fullRow.Add(GetCellValue(row + 1, column + 1));

					if (fullRow.IsWonTicTacToe())
					{
						return true;
					}

					fullRow = new List<string>();
					fullRow.Add(GetCellValue(row - 1, column + 1));
					fullRow.Add(FieldArray[row, column]);
					fullRow.Add(GetCellValue(row + 1, column - 1));

					if (fullRow.IsWonTicTacToe())
					{
						return true;
					}
				}
			}


			return false;
		}

		private string GetCellValue(int row, int column)
		{
			try
			{
				return FieldArray[row, column];
			}
			catch (Exception)
			{
				return null;
			}
		}

		private void SetRandomPlayerActive()
		{
			Players[random.Next(Players.Count)].SetActive();
		}

		protected virtual void OnEndContionReached(EndGameEventArgs e)
		{
			EventHandler handler = EndConditionReached;
			handler?.Invoke(this, e);

			IsFinished = true;
			EndState = e.EndState;
		}

		public void Reset()
		{
			IsFinished = false;
			FieldArray = new string[RowSize, ColumnSize];
			EndState = null;
		}
	}
}

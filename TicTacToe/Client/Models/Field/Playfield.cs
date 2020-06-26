using System;
using System.Collections.Generic;
using System.Threading;
using TicTacToe.Client.AI;
using TicTacToe.Client.Extensions;
using TicTacToe.Client.Models.Users;

namespace TicTacToe.Client.Models.Field
{
	public class Playfield
	{
		private Engine engine;

		private string[,] Field { get; set; }

		public Players Players { get; set; }

		public PlayFieldState State { get; set; }

		public Playfield(Players players)
		{
			if (players == null)
			{
				throw new ArgumentNullException(nameof(players));
			}

			Players = players;
			State = new PlayFieldState();
			engine = new Engine();
		}

		public string GetValue(int row, int column)
		{
			// Try-Catch is necessary for the win condition check
			try
			{
				return Field[row, column];
			}
			catch (Exception)
			{
				return null;
			}
		}

		public void Start()
		{
			ConfigureStartConditions();

			if (this.State.ActivePlayer.Type == PlayerType.Ai)
			{
				AIMove();
			}			
		}

		public void Reset()
		{
			Start();
		}

		public void ConfigureStartConditions()
		{
			this.Field = new string[3, 3];
			this.State.ActivePlayer = this.Players.GetRandomPlayer();
			this.State.IsFinished = false;
			this.State.IsStarted = true;
			this.State.EndState = null;
		}

		private void AIMove()
		{
			var postionMove = engine.Move(Field);
			InternalMove(postionMove.Row, postionMove.Column);
		}

		public void Move(int row, int column)
		{
			if (this.State.ActivePlayer.Type == PlayerType.Human)
			{
				if (ValidateMove(row, column))
				{
					this.Field[row, column] = this.State.ActivePlayer.Mark.ToString();
					if (EndConditionReached()) return;

					this.State.ActivePlayer = this.Players.GetNextPlayer(this.State.ActivePlayer);
				}
			}

			if (this.State.ActivePlayer.Type == PlayerType.Ai)
			{
				AIMove();
			}
		}

		private void InternalMove(int row, int column)
		{
			if (ValidateMove(row, column))
			{
				this.Field[row, column] = this.State.ActivePlayer.Mark.ToString();
				if (EndConditionReached()) return;

				this.State.ActivePlayer = this.Players.GetNextPlayer(this.State.ActivePlayer);
			}

			if (this.State.ActivePlayer.Type == PlayerType.Ai)
			{
				AIMove();
				this.State.ActivePlayer = this.Players.GetNextPlayer(this.State.ActivePlayer);
			}
		}

		private bool ValidateMove(int row, int column)
		{
			return string.IsNullOrEmpty(this.Field[row, column]);
		}


		private bool EndConditionReached()
		{
			if (EndConditionIsWon())
			{
				this.State.SetActivePlayerAsWinner();
			}
			else if (EndConditionIsTie())
			{
				this.State.SetTie();
			}

			return this.State.IsFinished;
		}

		private bool EndConditionIsWon()
		{
			// Winning conditions
			// X X X // X - - // X - -
			// - - - // - X - // X - -
			// - - - // - - X // X - -

			// Check if vertical winner
			for (var row = 0; Field.GetLength(0) > row; row++)
			{
				var fullRow = new List<string>();
				for (int column = 0; column < Field.GetLength(1); column++)
				{
					fullRow.Add(Field[row, column]);
				}

				if (fullRow.IsWonTicTacToe())
				{
					return true;
				}
			}

			// Check if horizontal winner
			for (var column = 0; Field.GetLength(1) > column; column++)
			{
				var fullRow = new List<string>();
				for (int row = 0; row < Field.GetLength(0); row++)
				{
					fullRow.Add(Field[row, column]);
				}

				if (fullRow.IsWonTicTacToe())
				{
					return true;
				}
			}

			// Check if diagonal winner
			for (int row = 0; row < Field.GetLength(0); row++)
			{
				List<string> fullRow;
				for (int column = 0; column < Field.GetLength(1); column++)
				{
					fullRow = new List<string>();

					fullRow.Add(GetValue(row - 1, column - 1));
					fullRow.Add(Field[row, column]);
					fullRow.Add(GetValue(row + 1, column + 1));

					if (fullRow.IsWonTicTacToe())
					{
						return true;
					}

					fullRow = new List<string>();
					fullRow.Add(GetValue(row - 1, column + 1));
					fullRow.Add(Field[row, column]);
					fullRow.Add(GetValue(row + 1, column - 1));

					if (fullRow.IsWonTicTacToe())
					{
						return true;
					}
				}
			}

			return false;
		}

		private bool EndConditionIsTie()
		{
			for (var row = 0; Field.GetLength(0) > row; row++)
			{
				for (int column = 0; column < Field.GetLength(1); column++)
				{
					if (string.IsNullOrEmpty(Field[row, column]))
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}

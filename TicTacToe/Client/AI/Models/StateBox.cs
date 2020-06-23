using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.AI.Models
{
	public class StateBox
	{
		Random Random = new Random();
		public StateBox(string[,] state)
		{
			State = state;
			Options = GenerateOptions();
		}

		public StateBox(string[,] state, List<Option> Options)
		{

		}

		public string[,] State { get; set; }

		public List<Option> Options { get; set; }

		public CellPosition GenerateMove()
		{
			var listCellPositionts = new List<CellPosition>();
			foreach (var option in Options)
			{
				for (int i = 0; i < option.Chance; i++)
				{
					listCellPositionts.Add(option.CellPosition);
				}
			}

			return listCellPositionts[Random.Next(100)];
		}

		private List<Option> GenerateOptions()
		{
			var options = new List<Option>();
			for (int row = 0; row < State.GetLength(0); row++)
			{
				for (int column = 0; column < State.GetLength(1); column++)
				{
					var cell = State[row, column];
					if (string.IsNullOrEmpty(cell))
					{
						options.Add(new Option()
						{
							CellPosition = new CellPosition(row, column),
						});
					}
				}
			}

			foreach (var option in options)
			{
				option.Chance = 100/options.Count;
			}

			return options;
		}
	}
}

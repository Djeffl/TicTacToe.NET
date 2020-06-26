using System.Collections.Generic;

namespace TicTacToe.Server.Domain
{
	public class PlayfieldState
	{
		public PlayfieldState(string[,] state)
		{
			State = state;
		}

		public int Id { get; set; }

		public string[,] State { get; set; }

		public List<PickOption> Options { get; set; }

		public void GenerateBaseOptions()
		{
			var options = new List<PickOption>();
			for (var row = 0; row < State.GetLength(0); row++)
			{
				for (int column = 0; column < State.GetLength(1); column++)
				{
					if (string.IsNullOrEmpty(State[row, column]))
					{
						Options.Add(new PickOption()
						{
							Position = new PlayfieldPosition(row, column)
						});
					}
				}
			}

			// Set all options to even odds
			foreach (var option in options)
			{
				option.Chance = 100/options.Count;
			}

			this.Options = options;
		}
	}
}

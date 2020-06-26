using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.Models.Users
{
	public class Players : List<Player>
	{
		private Random random = new Random();

		public Player GetRandomPlayer()
		{
			return this[random.Next(this.Count)];
		}

		public Player GetNextPlayer(Player currentPlayer)
		{
			var currentPlayerIndex = this.FindIndex(x => x.Id == currentPlayer.Id);

			if (currentPlayerIndex == this.Count() - 1)
			{
				return this.First();
			}
			else
			{
				return this[++currentPlayerIndex];
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.Models
{
	public class Players : List<Player>
	{
		public Player GetActivePlayer()
		{
			return this.Where(x => x.IsActive).FirstOrDefault();
		}

		public void SetNextActivePlayer()
		{
			var currentActivePlayer = GetActivePlayer();

			var nextPlayer = GetNextActivePlayer(currentActivePlayer);
			nextPlayer.SetActive();
			currentActivePlayer.SetInactive();
		}

		public Player GetNextActivePlayer(Player currentPlayer)
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

		public TicTacToeMark GetCurrentPlayerMark()
		{
			return GetActivePlayer().FieldMark;
		}
	}
}

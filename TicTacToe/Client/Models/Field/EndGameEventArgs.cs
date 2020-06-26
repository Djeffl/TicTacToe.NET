using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Client.Models.Users;

namespace TicTacToe.Client.Models.Field
{
	public class EndGameEventArgs : EventArgs
	{
		public EndGameEventArgs(EndState endState)
		{
			this.EndState = endState;
		}

		public EndGameEventArgs(EndState endState, Player winner)
		{
			this.EndState = endState;
			Winner = winner;
		}

		public EndState EndState { get; }
		public Player Winner { get; }
	}
}

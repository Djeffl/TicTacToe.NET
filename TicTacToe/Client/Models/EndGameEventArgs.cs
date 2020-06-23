using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.Models
{
	public class EndGameEventArgs : EventArgs
	{
		public EndGameEventArgs(EndState endState)
		{
			this.EndState = endState;
		}

		public EndState EndState { get; }
	}
}

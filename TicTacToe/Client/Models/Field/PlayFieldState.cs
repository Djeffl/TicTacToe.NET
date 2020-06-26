using System;
using TicTacToe.Client.Models.Users;

namespace TicTacToe.Client.Models.Field
{
	public class PlayFieldState
	{
		public Player ActivePlayer { get; set; }

		public Player Winner { get; set; }

		public bool IsStarted { get; set; }

		public bool IsFinished { get; set; }

		public EndState? EndState { get; set; }

		public event EventHandler EndConditionReached;

		protected virtual void OnEndContionReached(EndGameEventArgs e)
		{
			EventHandler handler = EndConditionReached;
			handler?.Invoke(this, e);

			IsFinished = true;
			EndState = e.EndState;
		}

		public void SetActivePlayerAsWinner()
		{
			this.Winner = this.ActivePlayer;
			this.EndState = Field.EndState.Win;
			this.IsFinished = true;
			OnEndContionReached(new EndGameEventArgs(Field.EndState.Win, this.Winner));
		}

		public void SetTie() {
			this.EndState = Field.EndState.Tie;
			this.IsFinished = true;
			OnEndContionReached(new EndGameEventArgs(Field.EndState.Tie));
		}
	}
}
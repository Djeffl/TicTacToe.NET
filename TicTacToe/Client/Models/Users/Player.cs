using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.Models.Users
{
	public class Player
	{
		public Player(string name, PlayerType type, TicTacToeMark mark)
		{
			this.Id = Guid.NewGuid();
			Name = name;
			Type = type;
			Mark = mark;
		}

		public Guid Id { get; set; }

		public string Name { get; set; }
		public int Score { get; set; }

		public TicTacToeMark Mark { get; set; }

		public PlayerType Type { get; set; }
	}
}

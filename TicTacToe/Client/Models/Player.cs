using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.Models
{
	public class Player
	{
		/// <summary>
		/// C'tor
		/// </summary>
		/// <param name="name">Name of the player</param>
		/// <param name="type">Defines if the player is an AI or human</param>
		/// <param name="fieldMark">Defines if the player is an X or an O</param>
		public Player(string name, PlayerType type, TicTacToeMark fieldMark)
		{
			Id = Guid.NewGuid();
			this.Name = name;
			this.Type = type;
			this.FieldMark = fieldMark;
		}

		public Guid Id { get; set; }

		public string Name { get; set; }

		public PlayerType Type { get; set; }

		public int Points { get; set; }

		public bool IsActive { get; set; }

		/// <summary>
		/// Eg.: X or O
		/// </summary>
		public TicTacToeMark FieldMark { get; set; }

		public void SetInactive()
		{
			IsActive = false;
		}

		public void SetActive()
		{
			IsActive = true;
		}
	}
}

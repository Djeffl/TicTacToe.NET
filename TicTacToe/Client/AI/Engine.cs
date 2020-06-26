using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TicTacToe.Client.AI.Models;

namespace TicTacToe.Client.AI
{
	public class Engine
	{
		public Engine()
		{
			BoardStates = new List<StateBox>();
		}
		public List<StateBox> BoardStates { get; set; }

		public CellPosition Move(string[,] field)
		{
			var stateBox = BoardStates.Where(x => x.State == field).FirstOrDefault();

			if (stateBox == null)
			{
				stateBox = new StateBox(field);
				BoardStates.Add(stateBox);
			}

			return stateBox.GenerateMove();
		}


	}
}

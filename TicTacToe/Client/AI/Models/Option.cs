using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.AI.Models
{
	public class Option
	{
		public CellPosition CellPosition { get; set; }
		public double Chance { get; set; }
	}
}

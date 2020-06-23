using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.Extensions
{
	public static class ListExtenstions
	{
		public static bool IsWonTicTacToe(this List<string> list)
		{
			var previousSymbol = "";
			var counterSame = 0;
			foreach (var s in list)
			{
				if (counterSame == 3) break; // 3 in a row is the winning condition!

				if (string.IsNullOrEmpty(previousSymbol) || s != previousSymbol)
				{
					counterSame = 1;
					previousSymbol = s;
				}
				else
				{
					++counterSame;
				}
			}

			return counterSame == 3;
		}
	}
}

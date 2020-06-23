using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Client.Exceptions
{
	public class InvalidMoveException : Exception
	{
		public InvalidMoveException(string message) : base(message)
		{

		}

		public InvalidMoveException()
		{

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Server.Dto.Request
{
	public class CreatePlayfieldStateRequestDto
	{
		public string[,] State { get; set; }
	}
}

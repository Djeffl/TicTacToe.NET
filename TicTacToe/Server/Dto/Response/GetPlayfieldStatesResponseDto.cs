using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Server.Dto.Response
{
	public class GetPlayfieldStatesResponseDto
	{
		public IEnumerable<GetPlayfieldStateResponseDto> PlayfieldStates { get; set; }
	}
}

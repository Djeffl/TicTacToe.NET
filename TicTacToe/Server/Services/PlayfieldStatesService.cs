using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Server.Domain;
using TicTacToe.Server.Models;
using TicTacToe.Server.Models.PlayfieldState;
using TicTacToe.Server.Repositories;

namespace TicTacToe.Server.Services
{
	public class PlayfieldStatesService
	{
		private readonly TicTacToeContext context;

		public PlayfieldStatesService(TicTacToeContext context)
		{
			this.context = context;
		}

		public async Task<List<PlayfieldState>> GetPlayfieldStatebyStatesAsync(PlayfieldStateCriteria criteria)
		{
			var queryable = context.PlayfieldStates.AsQueryable();
			if(criteria.State != null) queryable = queryable.Where(x => x.State == criteria.State);

			return await queryable.ToListAsync();
		}

		public async Task<PlayfieldState> CreatePlayfieldStateAsync(CreatePlayfieldState createPlayfieldState)
		{
			var playfieldState = new PlayfieldState(createPlayfieldState.State);
			playfieldState.GenerateBaseOptions();

			context.PlayfieldStates.Add(playfieldState);
			await context.SaveChangesAsync().ConfigureAwait(false);

			return playfieldState;
		}

		public async Task<PlayfieldState> GetPlayfieldStatebyStateByIdAsync(int id)
		{
			return await context.PlayfieldStates.FirstOrDefaultAsync(x => x.Id == id);
		}

		//public async Task<PlayfieldState> UpdatePlayFieldState(int id, PlayfieldState playfieldState)
		//{
		//	context.PlayfieldStates.Add(playfieldState);
		//	await context.SaveChangesAsync();

		//	return playfieldState;
		//}
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TicTacToe.Client.Models.Field;

namespace TicTacToe.Client.Services
{
	public class PlayfieldService
	{
		private readonly HttpClient client;

		public PlayfieldService(HttpClient client)
		{
			this.client = client;
		}

		public async Task<PlayFieldState> GetPlayFieldStateAsync(string[,] state)
		{

			var playfieldStates = await client.GetFromJsonAsync<List<PlayFieldState>>($"api/playfieldstates?state={JsonConvert.SerializeObject(state)}");

			return playfieldStates.FirstOrDefault();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Newtonsoft.Json;
using TicTacToe.Server.Domain;
using TicTacToe.Server.Dto.Request;
using TicTacToe.Server.Dto.Response;
using TicTacToe.Server.Models.PlayfieldState;
using TicTacToe.Server.Services;

namespace TicTacToe.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlayfieldStatesController : ControllerBase
	{
		// MODEL VALIDATION!!!
		// Todo: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.1

		private readonly PlayfieldStatesService playfieldStatesService;

		public PlayfieldStatesController(PlayfieldStatesService playfieldStatesService)
		{
			this.playfieldStatesService = playfieldStatesService;
		}


		[HttpGet()]
		public async Task<IActionResult> GetPlayfieldStates(string jsonState = null)
		{
			var criteria = new PlayfieldStateCriteria();
			if (!string.IsNullOrEmpty(jsonState))
			{
				criteria.State = JsonConvert.DeserializeObject<string[,]>(jsonState);
			}

			var playfieldStates = await playfieldStatesService.GetPlayfieldStatebyStatesAsync(criteria);
			
			var response = new GetPlayfieldStatesResponseDto()
			{
				PlayfieldStates = playfieldStates.Select(x => new GetPlayfieldStateResponseDto()
				{
					JsonState = JsonConvert.SerializeObject(x.State)
				})
			};

			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var playfieldState = await playfieldStatesService.GetPlayfieldStatebyStateByIdAsync(id);

			if (playfieldState == null)
			{
				return NotFound();
			}

			return Ok(playfieldState);
		}


		[HttpPost]
		public async Task<IActionResult> CreatePlayfieldState(CreatePlayfieldStateRequestDto createPlayfieldStateDto)
		{
			var createPlayfieldState = new CreatePlayfieldState()
			{
				State = createPlayfieldStateDto.State
			};

			var createdPlayfieldState = await this.playfieldStatesService.CreatePlayfieldStateAsync(createPlayfieldState);

			// https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
			return CreatedAtAction(nameof(GetById), new { id = createdPlayfieldState.Id }, createdPlayfieldState);
		}

		///// <summary>
		///// 
		///// </summary>
		///// <param name="patchDoc"></param>
		///// <returns></returns>
		///// https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-3.1
		//[HttpPatch]
		//public async Task<IActionResult> PatchPlayfieldState([FromBody]JsonPatchDocument<PlayfieldState> patchDoc)
		//{
		//	if (patchDoc != null)
		//	{
		//		var customer = new PlayfieldState(new string[3, 3]);

		//		patchDoc.ApplyTo(customer, ModelState);

		//		if (!ModelState.IsValid)
		//		{
		//			return BadRequest(ModelState);
		//		}

		//		return new ObjectResult(customer);
		//	}
		//	else
		//	{
		//		return BadRequest(ModelState);
		//	}
		//}
	}
}

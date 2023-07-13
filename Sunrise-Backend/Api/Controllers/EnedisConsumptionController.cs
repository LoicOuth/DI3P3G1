using System.Net;
using Application;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Domain.Model.Enedis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnedisConsumptionController : Controller
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	public EnedisConsumptionController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}
	/// <summary>
	/// Set Enedis Setting of current user {String Token, String PDM}
	/// </summary>
	/// <param name="data"></param>
	/// <returns>Ok or Error with Http code of error</returns>
	[Authorize]
	[HttpPost("SetSettings")]
	public async Task<IActionResult> SetEnedisSettings([FromBody] EnedisSetting data)
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var result  = await _mediator.Send(new UpdateEnedisSettingsCommand(data, usr));
			return Ok(result);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception)
		{
			return new StatusCodeResult(500);
		}
	}
	/// <summary>
	/// Get Token and PDM of current logged user
	/// </summary>
	/// <returns>EnedisSetting Object {String PDM ,String  Token}</returns>
	
	[Authorize]
	[HttpGet("GetSettings")]
	public async Task<IActionResult> GetEnedisSettings()
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var result  = await _mediator.Send(new GetEnedisSettingsQuery(usr));
			return Ok(result);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception)
		{
			return new StatusCodeResult(500);
		}
	}

	/// <summary>
	/// Get les data de conso de l'utilsateur en KW
	/// </summary>
	/// <param name="startDate" type="DateTime">Date de debut des data</param>
	/// <param name="endDate" type="DateTime">Date de fin des data</param>
	/// <returns></returns>
	[Authorize]
	[HttpGet("GetConsumptionData/{startDate}/{endDate}")]
	public async Task<IActionResult> GetConsuptionData(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var result = await _mediator.Send(new GetConsuptionDataQuery(startDate, endDate,usr ));
			return Ok(JsonSerializer.Serialize(result));
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.ToString());
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception)
		{
			return new StatusCodeResult(500);
		}
	}
	/// <summary>
	/// Getting consumption of yesterday 
	/// </summary>
	/// <returns></returns>
	//[Authorize]
	[HttpGet("Yesterday")]
	public async Task<IActionResult> GetConsuptionYesterday()
	{
		try
		{
			
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			User user = await _mediator.Send(new GetUserQuery(usr.Id));
			if (user.YesterdayEnedisConsumption != null && user.EnedisUpdatedAt.ToShortDateString() == DateTime.Now.ToShortDateString())
			{
				return Ok(user.YesterdayCustomConsumption);
			}
			return Ok(await _mediator.Send(new UpdateUserYesterdayEnedisConsumptionCommand(usr.Id)));
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception)
		{
			return new StatusCodeResult(500);
		}
	}
}
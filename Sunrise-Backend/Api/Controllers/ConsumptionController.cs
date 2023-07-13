using System.Reflection;
using Application;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Domain.Model.Stepper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsumptionController : Controller
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	public ConsumptionController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}
	
	/// <summary>
	/// Retrieve list of device
	/// </summary>
	/// <returns>List of device</returns>
	[Authorize]
	[HttpGet("stepperData")]
	public async Task<IActionResult> GetStepperData()
	{
		try
		{
			return Ok(new HomeDevice());
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
	/// Setup Consumption Setting and get estimated consumption in kwh
	/// </summary>
	/// <param name="data">Consumption settings</param>
	/// <returns>estimated consumption in kwh</returns>
	[Authorize]
	[HttpPost("SetSettings")]
	public async Task<IActionResult> SetSettings([FromBody] StepperInfo info)
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			await _mediator.Send(new UpdateConsumptionSettingsCommand(info, usr.Id));
			return Ok();
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
	///  Set consumption estimated /week
	/// </summary>
	/// <param name="model">Consumption in kwh</param>
	/// <returns>{}</returns>
	[Authorize]
	[HttpPost("SetEstimation")]
	public async Task<IActionResult> SetCustomConsumption([FromBody] double model)
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			return Ok(await _mediator.Send(new UpdateConsumptionCommand(model, usr.Id)));
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
	/// Get Estimated consumption settings of current user
	/// </summary>
	/// <returns>String of estimated consumption settings</returns>
	[Authorize]
	[HttpGet("settings")]
	public async Task<IActionResult> GetConsumptionSettings()
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			User user = await _mediator.Send(new GetUserQuery(usr.Id));
			return Ok(user.Consumption);
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
	/// Get Estimated consumption of the current user
	/// </summary>
	/// <returns>String of estimated consumption in kwh/week</returns>
	[Authorize]
	[HttpGet("Estimation")]
	public async Task<IActionResult> GetCustomEstimation()
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			User user = await _mediator.Send(new GetUserQuery(usr.Id));
			return Ok(user.Consumption.EstimatedConsumption);
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
	/// Getting random data to emulate consumption 
	/// </summary>
	/// <returns></returns>
	[Authorize]
	[HttpGet("Yesterday")]
	public async Task<IActionResult> GetCustomConsuptionYesterday()
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			User user = await _mediator.Send(new GetUserQuery(usr.Id));
			if (user.YesterdayCustomConsumption != null)
			{
				return Ok(user.YesterdayCustomConsumption);
			}
			return Ok(await _mediator.Send(new UpdateUserYesterdayConsumptionCommand(usr.Id)));
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
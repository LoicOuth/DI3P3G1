using Api.Common.DigitalTwin;
using Application;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IoTController : Controller
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	public IoTController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}
	/// <summary>
	/// Retrieve list of device
	/// </summary>
	/// <returns>List of device</returns>
	[Authorize]
	[HttpPut("updateTwin")]
	public async Task<IActionResult> UpdateTwin([FromBody] UpdateTwinBody body)
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var command = new UpdateDigitalTwinCommand(usr.DeviceId, body.IsChauffeEauEnabled);
			await _mediator.Send(command);
			return new OkResult();
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
	[Authorize]
	[HttpGet("getTwin")]
	public async Task<IActionResult> GetTwin()
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var command = new GetDigitalTwinCommand(usr.DeviceId);
			return new OkObjectResult(await _mediator.Send(command));
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

using Application;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeniusModeController : Controller
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	public GeniusModeController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[Authorize]
	[HttpGet("state")]
	public async Task<IActionResult> GetState()
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var result = await _mediator.Send(new GetStateQuery(usr));
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

	[Authorize]
	[HttpPut("UpdateState")]
	public async Task<IActionResult> UpdateState()
	{
		try
		{	UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var result = await _mediator.Send(new UpdateStateCommand(usr));
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

}
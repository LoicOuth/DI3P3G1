using Application;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeteoController : Controller
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	public MeteoController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}
	[Authorize]
	[HttpGet]
	public async Task<IActionResult> getMeteo()
	{
		try
		{ 
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			return Ok(await _mediator.Send(new GetMeteoQuery(usr)));
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
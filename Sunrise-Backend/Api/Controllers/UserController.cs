using Application;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Domain.Model.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SuperConvert.Extensions;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: Controller
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	public UserController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}	
	[HttpGet("{id}")]
	public async Task<IActionResult> GetUser(string id)
	{
		try
		{
			var result = await _mediator.Send(new GetUserQuery(id));
			return Ok(result.ToJson());
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
	[HttpGet()]
	public async Task<IActionResult> GetUsers()
	{
		try
		{
			var result = await _mediator.Send(new GetUsersQuery());
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
	[HttpPost()]
	public async Task<IActionResult> CreateUser([FromBody] CreateUserModel userModel)
	{
		try
		{
			var result = await _mediator.Send(new CreateUserCommand(userModel));
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
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateUser(string id, [FromBody]UpdateUserModel model )
	{
		try
		{
			var result = await _mediator.Send(new UpdateUserCommand(id,model));
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
	[HttpPut("device")]
	public async Task<IActionResult> UpdateUserDeviceID ([FromBody]string  model )
	{
		try
		{
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var result = await _mediator.Send(new UpdateUserDeviceIdCommand(usr , model));
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
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser(string id )
	{
		try
		{
			var result = await _mediator.Send(new DeleteUserCommand(id));
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
	[HttpGet("BI")]
	public async Task<IActionResult> GetAllForBi()
	{
		try
		{
			var result = await _mediator.Send(new GetUsersQuery());
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
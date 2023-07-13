using Api.Common.Auth;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Base;
using DnsClient.Protocol;
using Domain.Entities;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasicAuthController : Controller
{
	
	private readonly IMediator _mediator;
	private IUserService _userService;
	private readonly ILogger<BasicAuthController> _logger;
	public BasicAuthController(IMediator mediator,IUserService userService,ILogger<BasicAuthController> logger)
	{
		_userService = userService;
		_mediator = mediator;
		_logger = logger; 
	}
	
	[HttpPost("Login")]
	public async Task<IActionResult> Login(AuthenticateRequest model)
	{
		try
		{
			var response = await  _userService.Authenticate(model);

			if (response == null)
				return BadRequest(new { message = "Username or password is incorrect" });
			_logger.LogInformation(response.ToJson());
			return Ok(response);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
		
	}
	[Authorize]
	[HttpGet("me")]
	public IActionResult GetUserInfo()
	{
		var user = HttpContext.Items["User"];
		return Ok(user);
	}
}
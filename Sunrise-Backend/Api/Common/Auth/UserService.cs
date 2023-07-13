using System.Text.Json;
using Application;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using User = Domain.Entities.User;

namespace Api.Common.Auth;


using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public interface IUserService
{
	Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
	Task<UserDTO> GetById(string id);
}

public class UserService : IUserService
{

	private readonly AppSettings _appSettings;
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	public UserService(IOptions<AppSettings> appSettings, IMediator mediator, IMapper mapper)
	{
		_appSettings = appSettings.Value;
		_mediator = mediator;
		_mapper = mapper;
	}

	public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
	{
		try
		{
			var user = await _mediator.Send(new AuthenticateUserQuery(model.Email, model.Password));

			// return null if user not found
			if (user == null) return null;

			// authentication successful so generate jwt token
			var token = generateJwtToken(user);
			var at = new AuthenticateResponse(user, token);
			return at; 
		}
		catch(Exception e)
		{
			throw e;
		}
	    
	}
    
	public async Task<UserDTO> GetById(string id)
	{
		var usr = await _mediator.Send(new GetUserQuery(id));
		return _mapper.Map<UserDTO>(usr);
	}

	// helper methods

	private string generateJwtToken(UserDTO userDto)
	{
		// generate token that is valid for 7 days
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[] { new Claim("id", userDto.Id.ToString()) }),
			Expires = DateTime.UtcNow.AddDays(7),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}
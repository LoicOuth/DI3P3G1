using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Application;
public record AuthenticateUserQuery(string email, string password) : IRequest<UserDTO>;
public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserQuery>
{
	public AuthenticateUserValidator()
	{

	}
}
internal class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, UserDTO>
{
	private readonly ILogger<AuthenticateUserQueryHandler> _logger;
	private readonly IRepositoryUser _User;
	private readonly IMapper _mapper;
	public AuthenticateUserQueryHandler(ILogger<AuthenticateUserQueryHandler> logger,IRepositoryUser User, IMapper mapper )
	{
		_logger = logger;
		_User = User;
		_mapper = mapper;
	}
	public async Task<UserDTO> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var hashed_password = HashPassword(request.password); 
			var result = await _User.AuthenticateUser(request.email,hashed_password);
			return _mapper.Map<UserDTO>(result);
		}
		catch (Exception e)
		{
			_logger.LogError(e.Message, "LOG ERROR");
			throw;
		}
	}
	private string HashPassword(string strData)
	{
		var message = Encoding.UTF8.GetBytes(strData);
		using (var alg = SHA512.Create())
		{
			string hex = "";

			var hashValue = alg.ComputeHash(message);
			foreach (byte x in hashValue)
			{
				hex += String.Format("{0:x2}", x);
			}

			return hex;
		}
	}
}
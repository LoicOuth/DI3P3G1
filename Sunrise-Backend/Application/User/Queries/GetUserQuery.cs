using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Application;
public record GetUserQuery(string id) : IRequest<User>;
public class GetUserValidator : AbstractValidator<GetUserQuery>
{
	public GetUserValidator()
	{

	}
}
internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
	private readonly ILogger<GetUserQueryHandler> _logger;
	private readonly IRepositoryUser _user;
	public GetUserQueryHandler(ILogger<GetUserQueryHandler> logger,IRepositoryUser user)
	{
		_logger = logger;
		_user = user;
	}
	public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var result = await _user.GetUserAsync(request.id);
			return result;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
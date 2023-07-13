using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Application;
public record GetUsersQuery() : IRequest<List<User>>;
public class GetUsersValidator : AbstractValidator<GetUsersQuery>
{
	public GetUsersValidator()
	{

	}
}
internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
{
	private readonly ILogger<GetUsersQueryHandler> _logger;
	private readonly IRepositoryUser _user;
	public GetUsersQueryHandler(ILogger<GetUsersQueryHandler> logger,IRepositoryUser user)
	{
		_logger = logger;
		_user = user;
	}
	public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var users = await _user.GetUsersAsync();
			return users.ToList();
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
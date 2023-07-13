using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Model;
using Microsoft.Extensions.Logging;

namespace Application;
public record GetStateQuery(UserDTO user) : IRequest<bool>;
public class GetStateValidator : AbstractValidator<GetStateQuery>
{
	public GetStateValidator()
	{

	}
}
internal class GetStateHandler : IRequestHandler<GetStateQuery, bool>
{
	private readonly ILogger<GetStateHandler> _logger;
	private readonly IRepositoryUser _user;
	public GetStateHandler(ILogger<GetStateHandler> logger, IRepositoryUser user)
	{
		_logger = logger;
		_user = user;
		
	}
	public async Task<bool> Handle(GetStateQuery request, CancellationToken cancellationToken)
	{
		try
		{
			User usr = await _user.GetUserAsync(request.user.Id);
			return usr.GeniusMode;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
using Application.Common.Interfaces;
using Domain.Model;
using Microsoft.Extensions.Logging;

namespace Application;
public record GetUserConsumptionsDataQuery(UserDTO user) : IRequest<Unit>;
public class GetUserConsumptionsDataValidator : AbstractValidator<GetUserConsumptionsDataQuery>
{
	public GetUserConsumptionsDataValidator()
	{

	}
}
internal class GetUserConsumptionsDataHandler : IRequestHandler<GetUserConsumptionsDataQuery, Unit>
{
	private readonly ILogger<GetUserConsumptionsDataHandler> _logger;
	private readonly IRepositoryUser _user;
	public GetUserConsumptionsDataHandler(ILogger<GetUserConsumptionsDataHandler> logger)
	{
		_logger = logger;
	}
	public async Task<Unit> Handle(GetUserConsumptionsDataQuery request, CancellationToken cancellationToken)
	{
		try
		{
			return Unit.Value;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
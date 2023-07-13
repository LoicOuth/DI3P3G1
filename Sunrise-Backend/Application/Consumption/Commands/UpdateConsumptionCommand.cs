using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateConsumptionCommand(double value,string userId) : IRequest<User>;
public class UpdateConsumptionValidator : AbstractValidator<UpdateConsumptionCommand>
{
	public UpdateConsumptionValidator()
	{

	}
}
internal class UpdateConsumptionHandler : IRequestHandler<UpdateConsumptionCommand, User>
{
	private readonly ILogger<UpdateConsumptionHandler> _logger;
	private readonly IRepositoryUser _user;
	public UpdateConsumptionHandler(ILogger<UpdateConsumptionHandler> logger, IRepositoryUser user)
	{
		_logger = logger;
		_user = user; 
		
	}
	public async Task<User> Handle(UpdateConsumptionCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _user.GetUserAsync(request.userId);
			user.Consumption.EstimatedConsumption = request.value;
			await _user.UpdateAsync(user);
			return user;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
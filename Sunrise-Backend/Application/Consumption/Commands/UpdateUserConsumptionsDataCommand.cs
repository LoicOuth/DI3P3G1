using System.Data;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Helpers;
using Domain.Entities;
using Domain.Model;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateUserConsumptionsDataCommand(UserDTO user ) : IRequest<double>;
public class UpdateUserConsumptionsDataValidator : AbstractValidator<UpdateUserConsumptionsDataCommand>
{
	public UpdateUserConsumptionsDataValidator()
	{

	}
}
internal class UpdateUserConsumptionsDataHandler : IRequestHandler<UpdateUserConsumptionsDataCommand, double>
{
	private readonly ILogger<UpdateUserConsumptionsDataHandler> _logger;
	private readonly IUpdateConsumptionsDataHelper _helper;
	public UpdateUserConsumptionsDataHandler(ILogger<UpdateUserConsumptionsDataHandler> logger, IUpdateConsumptionsDataHelper helper)
	{
		_logger = logger;
		_helper = helper;
	}
	public async Task<double> Handle(UpdateUserConsumptionsDataCommand request, CancellationToken cancellationToken)
	{
		try
		{
			
			return await _helper.Helper(request.user);
			
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
		
	}
	private  double GetRandomNumber(double minimum, double maximum)
	{ 
		Random random = new Random();
		return random.NextDouble() * (maximum - minimum) + minimum;
	}
}
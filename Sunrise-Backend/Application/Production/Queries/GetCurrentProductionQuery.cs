using Application.Common.Interfaces;
using Application.Common.Interfaces.Helpers;
using Domain.Model;
using Microsoft.Extensions.Logging;

namespace Application;
public record GetCurrentProductionQuery(UserDTO user) : IRequest<double>;
public class GetCurrentProductionValidator : AbstractValidator<GetCurrentProductionQuery>
{
	public GetCurrentProductionValidator()
	{
		RuleFor(p => p.user.DeviceId).NotEmpty().WithMessage("Your profil need to be linked to DeviceID");
	}
}
internal class GetCurrentProductionHandler : IRequestHandler<GetCurrentProductionQuery, double>
{
	private readonly ILogger<GetCurrentProductionHandler> _logger;
	private readonly IGetCurrentProductionHelper _helper;
	
	public GetCurrentProductionHandler(ILogger<GetCurrentProductionHandler> logger, IGetCurrentProductionHelper helper)
	{
		_logger = logger;
		_helper = helper;
	}
	public async Task<double> Handle(GetCurrentProductionQuery request, CancellationToken cancellationToken)
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
}
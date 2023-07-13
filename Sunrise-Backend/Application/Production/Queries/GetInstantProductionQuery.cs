using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Model;
using Microsoft.Azure.Amqp;
using Microsoft.Extensions.Logging;

namespace Application;
public record GetInstantProductionQuery(UserDTO user) : IRequest<double>;
public class GetInstantProductionValidator : AbstractValidator<GetInstantProductionQuery>
{
	public GetInstantProductionValidator()
	{
		RuleFor(p => p.user.DeviceId).NotEmpty().WithMessage("Your profil need to be linked to DeviceID");
	}
}
internal class GetInstantProductionHandler : IRequestHandler<GetInstantProductionQuery, double>
{
	private readonly ILogger<GetInstantProductionHandler> _logger;
	private readonly IRepositoryIoTData _data; 
	public GetInstantProductionHandler(ILogger<GetInstantProductionHandler> logger, IRepositoryIoTData data)
	{
		_logger = logger;
		_data = data;
	}
	public async Task<double> Handle(GetInstantProductionQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var productions = await _data.GetAnyDataAsync();
			productions = productions.Where(p => p.IoTHub.ConnectionDeviceId == request.user.DeviceId);
			List<IoTData> prodList = productions.ToList();
			if (prodList.Count == 0)
			{
				return 0;
			}
			var toto = prodList[prodList.Count - 1];
			var res = (prodList[prodList.Count -1 ].voltage * 320) / 100;
			return res;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
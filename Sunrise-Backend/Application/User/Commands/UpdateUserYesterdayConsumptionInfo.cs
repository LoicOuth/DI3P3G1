using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Domain.Model.User;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateUserYesterdayConsumptionCommand(string id) : IRequest<List<ConsumptionInfo>>;
public class UpdateUserYesterdayConsumptionValidator : AbstractValidator<UpdateUserYesterdayConsumptionCommand>
{
	public UpdateUserYesterdayConsumptionValidator()
	{

	}
}
internal class UpdateUserYesterdayConsumptionHandler : IRequestHandler<UpdateUserYesterdayConsumptionCommand, List<ConsumptionInfo>>
{
	private readonly ILogger<UpdateUserYesterdayConsumptionHandler> _logger;
	private readonly IRepositoryUser _repositoryUser;
	private readonly IMapper _mapper;
	public UpdateUserYesterdayConsumptionHandler(ILogger<UpdateUserYesterdayConsumptionHandler> logger, IRepositoryUser repositoryUser, IMapper mapper)
	{
		_logger = logger;
		_repositoryUser = repositoryUser;
		_mapper = mapper;
	}
	public async Task<List<ConsumptionInfo>> Handle(UpdateUserYesterdayConsumptionCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _repositoryUser.GetUserAsync(request.id);
			List<ConsumptionInfo> conso = new List<ConsumptionInfo>();
			for (int i = 0; i < 24; i++)
			{
				conso.Add(new ConsumptionInfo($"{i}H", GetRandomNumber(0, 2)));
			}

			user.YesterdayCustomConsumption = conso;
			await _repositoryUser.UpdateAsync(user);
			return conso;
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
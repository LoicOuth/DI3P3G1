using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Domain.Model.User;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateUserYesterdayEnedisConsumptionCommand(string id) : IRequest<List<ConsumptionInfo>>;
public class UpdateUserYesterdayEnedisConsumptionValidator : AbstractValidator<UpdateUserYesterdayEnedisConsumptionCommand>
{
	public UpdateUserYesterdayEnedisConsumptionValidator()
	{

	}
}
internal class UpdateUserYesterdayEnedisConsumptionHandler : IRequestHandler<UpdateUserYesterdayEnedisConsumptionCommand, List<ConsumptionInfo>>
{
	private readonly ILogger<UpdateUserYesterdayEnedisConsumptionHandler> _logger;
	private readonly IRepositoryUser _repositoryUser;
	private readonly IEnedisConsumptionService _enedis; 
	private readonly IMapper _mapper;
	public UpdateUserYesterdayEnedisConsumptionHandler(ILogger<UpdateUserYesterdayEnedisConsumptionHandler> logger, IEnedisConsumptionService enedis,  IRepositoryUser repositoryUser, IMapper mapper)
	{
		_logger = logger;
		_repositoryUser = repositoryUser;
		_mapper = mapper;
		_enedis = enedis;
	}
	public async Task<List<ConsumptionInfo>> Handle(UpdateUserYesterdayEnedisConsumptionCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _repositoryUser.GetUserAsync(request.id);
			var yesterday = DateTime.Today.AddDays(-1);
			if (user.Token == null || user.PDM == null ||user.Token =="" || user.PDM=="")
			{
				throw new NotFoundException("Enedis Config not exist");
			}
			var consoList =  await _enedis.GetConsumptionData(yesterday , DateTime.Now , user.PDM, user.Token,  cancellationToken);
			List<ConsumptionInfo> resultList = new List<ConsumptionInfo>();
			foreach (var conso in consoList)
			{
				int hour = DateTime.Parse(conso.Date).Hour;
				resultList.Add(new ConsumptionInfo($"{hour}H", Double.Parse(conso.Value)/ 1000));
			}

			user.YesterdayEnedisConsumption = resultList;
			user.EnedisUpdatedAt = DateTime.Now;
			await _repositoryUser.UpdateAsync(user);
			return resultList;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
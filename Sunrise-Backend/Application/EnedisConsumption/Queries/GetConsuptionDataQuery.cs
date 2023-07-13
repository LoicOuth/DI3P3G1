using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Model;
using Microsoft.Extensions.Logging;
using Consumption = Domain.Model.Consumption;

namespace Application;

public record GetConsuptionDataQuery(DateTime start, DateTime end, UserDTO user) : IRequest<List<Domain.Model.Consumption>>;
public class GetConsuptionDataValidator : AbstractValidator<GetConsuptionDataQuery>
{
	public GetConsuptionDataValidator()
	{

	}
}
internal class GetConsuptionDataHandler : IRequestHandler<GetConsuptionDataQuery, List<Domain.Model.Consumption>>
{
	private readonly ILogger<GetConsuptionDataHandler> _logger;
	private readonly IEnedisConsumptionService _enedisConsumptionService;
	private readonly IRepositoryUser _user;
	public GetConsuptionDataHandler(ILogger<GetConsuptionDataHandler> logger, IEnedisConsumptionService enedisConsumptionService, IRepositoryUser user)
	{
		_enedisConsumptionService = enedisConsumptionService;
		_logger = logger;
		_user = user;
	}
	public async Task<List<Domain.Model.Consumption>> Handle(GetConsuptionDataQuery request, CancellationToken cancellationToken)
	{
		try
		{
			User user = await _user.GetUserAsync(request.user.Id);
			if (user.Token == null || user.PDM == null ||user.Token =="" || user.PDM=="")
			{
				throw new NotFoundException("Enedis Config not exist");
			}
			return await _enedisConsumptionService.GetConsumptionData(request.start , request.end , user.PDM, user.Token,  cancellationToken);

		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
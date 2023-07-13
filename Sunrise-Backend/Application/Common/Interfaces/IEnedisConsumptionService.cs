using Domain.Model;

namespace Application.Common.Interfaces;

public interface IEnedisConsumptionService
{
	Task<List<Domain.Model.Consumption>> GetConsumptionData(DateTime startDate, DateTime endDate, string PDM, string token , CancellationToken cancellationToken);

}
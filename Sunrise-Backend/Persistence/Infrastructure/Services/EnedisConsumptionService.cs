using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Model;
using EnedisConsumptionGateway;

namespace Persistence.Infrastructure.Services;

public class EnedisConsumptionService : IEnedisConsumptionService
{
	private HttpClient _client;
	private IMapper _mapper;

	public EnedisConsumptionService(IHttpClientFactory client, IMapper mapper)
	{
		_client = client.CreateClient("enedisGateway");
		_mapper = mapper;
	}
	/// <summary>
	/// Recupère une liste d'element correspondant a la consommation de l'utilisateur pendant un interval de temps 
	/// </summary>
	/// <param name="startDate"></param>
	/// <param name="endDate"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>List de consomation en WH </returns>
	/// <exception cref="NotFoundException"></exception>
	public async Task<List<Consumption>> GetConsumptionData(DateTime startDate, DateTime endDate, string PDM, string token,
		CancellationToken cancellationToken)
	{
		EnedisConsumption ec = new EnedisConsumption(_client);
		API_BODY body = new API_BODY()
		{
			Start = startDate.ToString("yyyy-MM-dd"),
			End = endDate.ToString("yyyy-MM-dd"),
			Type = API_BODYType.Consumption_load_curve,
			Usage_point_id = PDM,
		};
		var response = await ec.ApiAsync(token, body,
			cancellationToken);
		if (response.Meter_reading != null)
		{
			List<Consumption> data = new List<Consumption>();
			var consoTotal = 0;
			foreach (var intervalReading in response.Meter_reading.Interval_reading)
			{
				consoTotal += intervalReading.Value;
				if (DateTime.Parse(intervalReading.Date).Minute == 00)
				{
					intervalReading.Value = consoTotal;
					data.Add(_mapper.Map<Interval_reading, Consumption>(intervalReading));
					consoTotal = 0;
				}
			}
			return data;
		}
		throw new NotFoundException();
	}
}
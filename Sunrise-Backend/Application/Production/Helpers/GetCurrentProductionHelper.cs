using Application.Common.Interfaces;
using Application.Common.Interfaces.Helpers;
using Domain.Model;

namespace Application.Production.Helpers;

public class GetCurrentProductionHelper : IGetCurrentProductionHelper
{
	private readonly IRepositoryIoTData _data; 
	public  GetCurrentProductionHelper(IRepositoryIoTData data)
	{
		_data = data;
	}

	public async Task<double> Helper(UserDTO user)
	{
		var productions = await _data.GetAnyDataAsync();
		productions = productions.Where(p => p.IoTHub.ConnectionDeviceId == user.DeviceId);
		productions = productions.Where(p => p.EventProcessedUtcTime.ToShortDateString() == DateTime.Now.ToShortDateString());
		double value = 0;
		int counter = 0;
		foreach (var prod in productions)
		{
			// Transformation du % en valeur en W
			// En estimant que 100% = 320W (Valeur de production maximal moyenne d'un panneau solaire)
			value += (prod.voltage * 320)/100;
			counter++;
		}

		if (value == 0) return 0;
		// Transformation en KW
		value =(value / counter);
		double productionTotal = value/1000;
		return productionTotal;
	}
}
using Application.Common.Interfaces.Helpers;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Configuration;

namespace Application.Helpers;

public class GetDigitalTwinHelper : IGetDigitalTwinHelper
{
	private readonly IConfiguration _configuration;
	private RegistryManager _registryManager;
	public GetDigitalTwinHelper(IConfiguration configuration)
	{
		_configuration = configuration;
		_registryManager = RegistryManager.CreateFromConnectionString(_configuration["AzureIoT:ConnectionString"]);
		if (_registryManager == null)
		{
			throw new ValidationException($"Invalid AzureIoT ConnectionString in configuration");
		}
	}

	public async Task<bool> Helper(string Id)
	{
		Twin twin = await _registryManager.GetTwinAsync(Id) ?? throw new ValidationException($"Invalid DeviceId provided {Id} for {_configuration["AzureIoT:ConnectionString"]}");
		return twin.Properties.Desired["chauffeEauEnabled"].Value;
	} 
}
using Application.Common.Interfaces.Helpers;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Configuration;

namespace Application.Helpers;

public class UpdateDigitalTwinHelper : IUpdateDigitalTwinHelper
{
	private readonly IConfiguration _configuration;
	private readonly RegistryManager _registryManager;
	public UpdateDigitalTwinHelper(IConfiguration configuration)
	{
		_configuration = configuration;
		_registryManager = RegistryManager.CreateFromConnectionString(_configuration["AzureIoT:ConnectionString"]);
		if (_registryManager == null)
		{
			throw new ValidationException($"Invalid AzureIoT ConnectionString in configuration");
		}
	}

	public async Task<Unit> Helper(string id, bool enabled)
	{
		Twin twin = await _registryManager.GetTwinAsync(id) ?? throw new ValidationException($"Invalid DeviceId provided {id} for {_configuration["AzureIoT:ConnectionString"]}");
		twin.Properties.Desired["chauffeEauEnabled"] = enabled;
		await _registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);
		return Unit.Value;
	}
}
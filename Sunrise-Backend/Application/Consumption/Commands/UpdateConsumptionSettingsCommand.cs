using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Model.Stepper;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateConsumptionSettingsCommand(StepperInfo data, string userId) : IRequest<Unit>;
public class UpdateConsumptionSettingsValidator : AbstractValidator<UpdateConsumptionSettingsCommand>
{
	public UpdateConsumptionSettingsValidator()
	{

	}
}
internal class UpdateConsumptionSettingsHandler : IRequestHandler<UpdateConsumptionSettingsCommand, Unit>
{
	private readonly ILogger<UpdateConsumptionSettingsHandler> _logger;
	private readonly IRepositoryUser _user;
	public UpdateConsumptionSettingsHandler(ILogger<UpdateConsumptionSettingsHandler> logger, IRepositoryUser user)
	{
		_logger = logger;
		_user = user; 
		
	}
	public async Task<Unit> Handle(UpdateConsumptionSettingsCommand request, CancellationToken cancellationToken)
	{
		try
		{
			
			var model = request.data;
			var user = await _user.GetUserAsync(request.userId);
			Domain.Entities.Consumption consumption = new Domain.Entities.Consumption();
			consumption.StartDate = TimeSpan.Parse(model.StartDate);
			consumption.EndDate = TimeSpan.Parse(model.EndDate);
			List<Device> selectedDevices = new List<Device>();
			List<Device> deviceList = new HomeDevice().devices;
			foreach (var device in model.Device)
			{
				selectedDevices.Add(deviceList.Find(p => p.Id == device));
			}
			consumption.Device = selectedDevices;
			consumption.PeopleNumber = model.PeopleNumber;
			consumption.WashingNumber = model.WashingNumber;
			consumption.EstimatedConsumption = double.Parse(ConsumptionEstimate(model));
			user.Consumption = consumption;
			await _user.UpdateAsync(user);
			return Unit.Value;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
	/// <summary>
	/// Calculating the total consumption in kwh by user information
	/// </summary>
	/// <param name="data">consumption settings</param>
	/// <returns>consumption in kwh</returns>
	private string ConsumptionEstimate(StepperInfo data)
	{
		double total = 0.0 ;
		int nbPerson = data.PeopleNumber;
		List<Device> devices = new HomeDevice().devices;
		foreach (var device in data.Device)
		{
			var selectedDevice = devices.Find(p => p.Id == device);
			if ( selectedDevice != null)
			{

				if (device == "WashingMachine")
				{
					total += selectedDevice.Value * nbPerson * data.WashingNumber;
				}
				else
				{
					total += selectedDevice.Value * nbPerson;
				}
			}
		}
		return total.ToString();
	}
}
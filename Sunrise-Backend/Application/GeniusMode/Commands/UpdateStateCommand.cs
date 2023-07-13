using Application.Common.Interfaces;
using Application.Common.Interfaces.Helpers;
using Domain.Entities;
using Domain.Model;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateStateCommand(UserDTO user) : IRequest<bool>;
public class UpdateStateValidator : AbstractValidator<UpdateStateCommand>
{
	public UpdateStateValidator()
	{

	}
}
internal class UpdateStateHandler : IRequestHandler<UpdateStateCommand, bool>
{
	private readonly ILogger<UpdateStateHandler> _logger;
	private readonly IGetCurrentProductionHelper _prodHelper;
	private readonly IUpdateConsumptionsDataHelper _consHelper;
	private readonly IUpdateDigitalTwinHelper _twinHelper; 
	private readonly IRepositoryUser _user;
	private readonly IGetDigitalTwinHelper _getDigitalTwin;
	
	
	public UpdateStateHandler(ILogger<UpdateStateHandler> logger,IRepositoryUser user,IGetDigitalTwinHelper getDigitalTwin, IUpdateDigitalTwinHelper twinHelper ,  IGetCurrentProductionHelper prodHelper, IUpdateConsumptionsDataHelper consHelper)
	{
		_logger = logger;
		_prodHelper = prodHelper;
		_consHelper = consHelper;
		_user = user;
		_twinHelper = twinHelper;
		_getDigitalTwin = getDigitalTwin;
	}
	public async Task<bool> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
	{
		try
		{
			User usr = await _user.GetUserAsync(request.user.Id);
			if (usr.GeniusMode == null)
			{
				usr.GeniusMode = true;
			}
			else
			{
				usr.GeniusMode = usr.GeniusMode == true ? false : true; 
			}
			await _user.UpdateAsync(usr);
			if (usr.GeniusMode == true)
			{
				Thread thread = new Thread(()=> startThreadVonsoEco(request.user));
				thread.Start();
			}
			return usr.GeniusMode;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}

	private async Task startThreadVonsoEco(UserDTO user )
	{
		bool state = true;
		while (state == true)
		{
			var usr = await _user.GetUserAsync(user.Id);
			state = usr.GeniusMode;
			double currentConsumption = await _consHelper.Helper(user);
			double production = await _prodHelper.Helper(user);
			double autonomy = calcAutonomy(production,currentConsumption);

			if (autonomy > 50)
			{
				if (!await _getDigitalTwin.Helper(usr.DeviceId))
				{
					await _twinHelper.Helper(usr.DeviceId, true);
				}
			}
			else
			{
				if (await _getDigitalTwin.Helper(usr.DeviceId))
				{
					await _twinHelper.Helper(usr.DeviceId, false);
				}
			}
		}
	}
	private double calcAutonomy(double prod, double consum)
	{
		if (consum >= prod)
		{
			return 0;
		}
		
		return 100 - ((consum / prod) * 100) ;
	}
	private  double GetRandomNumber(double minimum, double maximum)
	{ 
		Random random = new Random();
		return random.NextDouble() * (maximum - minimum) + minimum;
	}
}
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Model;
using Domain.Model.Enedis;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateEnedisSettingsCommand(EnedisSetting model , UserDTO user) : IRequest<User>;
public class UpdateEnedisSettingsValidator : AbstractValidator<UpdateEnedisSettingsCommand>
{
	private readonly IEnedisConsumptionService _enedis;
	public UpdateEnedisSettingsValidator(IEnedisConsumptionService enedis)
	{
		_enedis = enedis;
		RuleFor(p => p.model.Token).NotEmpty().WithMessage("Token is required");
		RuleFor(p => p.model.PDM).NotEmpty().WithMessage("PDM is riquired");
		RuleFor(p => p.model).MustAsync(isValidSetting).WithMessage("Invalid configuration");
	}

	private async Task<bool> isValidSetting(EnedisSetting model, CancellationToken cancellationToken)
	{
		try
		{
			DateTime startDate = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month - 1}-01");
			DateTime endDate = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month - 1}-03");
			await _enedis.GetConsumptionData(startDate, endDate , model.PDM, model.Token, cancellationToken);
			return true;
		}
		catch
		{
			return false; 
		}
	}
}
internal class UpdateEnedisSettingsHandler : IRequestHandler<UpdateEnedisSettingsCommand, User>
{
	private readonly ILogger<UpdateEnedisSettingsHandler> _logger;
	private readonly IRepositoryUser _user;
	public UpdateEnedisSettingsHandler(ILogger<UpdateEnedisSettingsHandler> logger, IRepositoryUser user)
	{
		_logger = logger;
		_user = user;
	}
	public async Task<User> Handle(UpdateEnedisSettingsCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _user.GetUserAsync(request.user.Id);
			user.Token = request.model.Token;
			user.PDM = request.model.PDM;
			await _user.UpdateAsync(user);
			return user;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
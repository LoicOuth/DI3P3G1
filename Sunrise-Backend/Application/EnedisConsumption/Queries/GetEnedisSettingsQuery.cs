using Application.Common.Interfaces;
using Domain.Model;
using Domain.Model.Enedis;
using Microsoft.Extensions.Logging;

namespace Application;
public record GetEnedisSettingsQuery(UserDTO user) : IRequest<EnedisSetting>;
public class GetEnedisSettingsValidator : AbstractValidator<GetEnedisSettingsQuery>
{
	public GetEnedisSettingsValidator()
	{

	}
}
internal class GetEnedisSettingsHandler : IRequestHandler<GetEnedisSettingsQuery, EnedisSetting>
{
	private readonly ILogger<GetEnedisSettingsHandler> _logger;
	private readonly IRepositoryUser _user;
	public GetEnedisSettingsHandler(ILogger<GetEnedisSettingsHandler> logger , IRepositoryUser user)
	{
		_logger = logger;
		_user = user;
	}
	public async Task<EnedisSetting> Handle(GetEnedisSettingsQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _user.GetUserAsync(request.user.Id);
			EnedisSetting setting = new EnedisSetting() { Token = user.Token, PDM = user.PDM };
			return setting;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
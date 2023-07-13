using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Model;
using Microsoft.Extensions.Logging;

namespace Application;
public record GetMeteoQuery(UserDTO user) : IRequest<CurrentCondition>;
public class GetMeteoValidator : AbstractValidator<GetMeteoQuery>
{
	public GetMeteoValidator()
	{

	}
}
internal class GetMeteoHandler : IRequestHandler<GetMeteoQuery, CurrentCondition>
{
	private readonly ILogger<GetMeteoHandler> _logger;
	private readonly IRepositoryUser _user;
	private readonly IMeteoService _meteo;
	public GetMeteoHandler(ILogger<GetMeteoHandler> logger, IRepositoryUser user, IMeteoService meteo)
	{
		_logger = logger;
		_user = user;
		_meteo = meteo;
	}
	public async Task<CurrentCondition> Handle(GetMeteoQuery request, CancellationToken cancellationToken)
	{
		try
		{
			User user = await _user.GetUserAsync(request.user.Id);
			return  await _meteo.GetCurrentMeteo(user.Address.City);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
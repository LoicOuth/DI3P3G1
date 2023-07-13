using Application.Common.Interfaces.Helpers;
using Microsoft.Extensions.Logging;

namespace Application;
public record GetDigitalTwinCommand(string DeviceId) : IRequest<bool>;
public class GetDigitalTwinValidator : AbstractValidator<GetDigitalTwinCommand>
{
	public GetDigitalTwinValidator()
	{
		RuleFor(x => x.DeviceId).NotEmpty();
	}
}
internal class GetDigitalTwinHandler : IRequestHandler<GetDigitalTwinCommand, bool>
{
	private readonly ILogger<GetDigitalTwinHandler> _logger;
	private readonly IGetDigitalTwinHelper _helper;
	public GetDigitalTwinHandler(ILogger<GetDigitalTwinHandler> logger, IGetDigitalTwinHelper helper)
	{
		_logger = logger;
		_helper = helper;
	}
	public async Task<bool> Handle(GetDigitalTwinCommand request, CancellationToken cancellationToken)
	{
		try
		{
			return await _helper.Helper(request.DeviceId);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
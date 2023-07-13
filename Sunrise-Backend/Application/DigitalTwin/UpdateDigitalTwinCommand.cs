using Application.Common.Interfaces.Helpers;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateDigitalTwinCommand(string DeviceId, bool IsChauffeEauEnabled) : IRequest<Unit>;
public class UpdateDigitalTwinValidator : AbstractValidator<UpdateDigitalTwinCommand>
{
	public UpdateDigitalTwinValidator()
	{
		RuleFor(x => x.DeviceId).NotEmpty();
	}
}
internal class UpdateDigitalTwinHandler : IRequestHandler<UpdateDigitalTwinCommand, Unit>
{
	private readonly ILogger<UpdateDigitalTwinHandler> _logger;
	private readonly IUpdateDigitalTwinHelper _helper;
	public UpdateDigitalTwinHandler(ILogger<UpdateDigitalTwinHandler> logger, IUpdateDigitalTwinHelper helper)
	{
		_logger = logger;
		_helper = helper;
	}
	public async Task<Unit> Handle(UpdateDigitalTwinCommand request, CancellationToken cancellationToken)
	{
		try
		{
			await _helper.Helper(request.DeviceId, request.IsChauffeEauEnabled);
			return Unit.Value;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
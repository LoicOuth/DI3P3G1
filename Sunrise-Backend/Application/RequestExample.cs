using Microsoft.Extensions.Logging;

namespace Application;
public record RequestExampleQuery() : IRequest<Unit>;
public class RequestExampleValidator : AbstractValidator<RequestExampleQuery>
{
	public RequestExampleValidator()
	{

	}
}
internal class RequestExampleHandler : IRequestHandler<RequestExampleQuery, Unit>
{
	private readonly ILogger<RequestExampleHandler> _logger;
	public RequestExampleHandler(ILogger<RequestExampleHandler> logger)
	{
		_logger = logger;
	}
	public async Task<Unit> Handle(RequestExampleQuery request, CancellationToken cancellationToken)
	{
		try
		{
			return Unit.Value;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviour(
        ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();

        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        var requestName = typeof(TRequest).Name;
        if (elapsedMilliseconds > 500)
        {
            
            
            _logger.LogWarning("Request Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds){@Request}",
                requestName, elapsedMilliseconds, request);
        }
        _logger.LogWarning("Request Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds){@Request} we can thanks damien for the code optimisation",
            requestName, elapsedMilliseconds, request);
        return response;
    }
}
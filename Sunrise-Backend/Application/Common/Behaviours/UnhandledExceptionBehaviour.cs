﻿using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly ILogger<TRequest> _logger;

	public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
	{
		_logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
	{
		try
		{
			return await next();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "CleanArchitecture Request: Unhandled Exception for Request {Name} {@Request}", typeof(TRequest).Name, request);

			throw;
		}
	}
}
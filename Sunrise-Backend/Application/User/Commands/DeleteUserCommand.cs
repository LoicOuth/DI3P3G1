using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application;
public record DeleteUserCommand(string id) : IRequest<Unit>;
public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
	public DeleteUserValidator()
	{

	}
}
internal class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
{
	private readonly ILogger<DeleteUserHandler> _logger;
	private readonly IRepositoryUser _repositoryUser;
	public DeleteUserHandler(ILogger<DeleteUserHandler> logger, IRepositoryUser repositoryUser)
	{
		_logger = logger;
		_repositoryUser = repositoryUser;
	}
	public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			await _repositoryUser.DeleteAsync(request.id);
			return Unit.Value;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
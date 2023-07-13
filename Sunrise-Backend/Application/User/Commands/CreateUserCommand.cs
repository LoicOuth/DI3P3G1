using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Model.User;
using Microsoft.Extensions.Logging;

namespace Application;
public record CreateUserCommand(CreateUserModel userModel) : IRequest<Unit>;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserValidator()
	{

	}
}
internal class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
{
	private readonly ILogger<CreateUserHandler> _logger;
	private readonly IRepositoryUser _user;
	private readonly IMapper _mapper; 
	public CreateUserHandler(ILogger<CreateUserHandler> logger , IRepositoryUser user, IMapper mapper)
	{
		_user = user;
		_logger = logger;
		_mapper = mapper;
	}
	public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var model = request.userModel;
			var newUser = _mapper.Map<CreateUserModel, User>(model);
			await _user.InsertAsync(newUser);	
			return Unit.Value;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
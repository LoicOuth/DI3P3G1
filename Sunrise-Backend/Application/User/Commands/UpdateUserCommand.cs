using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Model.User;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateUserCommand(string id , UpdateUserModel UserModel) : IRequest<Unit>;
public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
	public UpdateUserValidator()
	{

	}
}
internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
{
	private readonly ILogger<UpdateUserHandler> _logger;
	private readonly IRepositoryUser _repositoryUser;
	private readonly IMapper _mapper;
	public UpdateUserHandler(ILogger<UpdateUserHandler> logger, IRepositoryUser repositoryUser, IMapper mapper)
	{
		_logger = logger;
		_repositoryUser = repositoryUser;
		_mapper = mapper;
	}
	public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var model = request.UserModel;
			var user = await _repositoryUser.GetUserAsync(request.id);
			user.Address = model.Address;
			user.Email = model.Email;
			user.LastName = model.LastName;
			user.FirstName = model.FirstName;
			user.Password = model.Password;
			user.Token = model.Token;
			user.PDM = model.PDM;
			await _repositoryUser.UpdateAsync(user);
			return Unit.Value;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
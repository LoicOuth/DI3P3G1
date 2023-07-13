using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Microsoft.Extensions.Logging;

namespace Application;
public record UpdateUserDeviceIdCommand(UserDTO user ,string deviceID) : IRequest<UserDTO>;
public class UpdateUserDeviceIdValidator : AbstractValidator<UpdateUserDeviceIdCommand>
{
	public UpdateUserDeviceIdValidator()
	{

	}
}
internal class UpdateUserDeviceIdHandler : IRequestHandler<UpdateUserDeviceIdCommand, UserDTO>
{
	private readonly ILogger<UpdateUserDeviceIdHandler> _logger;
	private readonly IRepositoryUser _user;
	private readonly IMapper _mapper;
	public UpdateUserDeviceIdHandler(ILogger<UpdateUserDeviceIdHandler> logger, IRepositoryUser user , IMapper mapper)
	{
		_logger = logger;
		_user = user;
		_mapper = mapper;
	}
	public async Task<UserDTO> Handle(UpdateUserDeviceIdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			User usr = await _user.GetUserAsync(request.user.Id);
			usr.DeviceId = request.deviceID;
			await _user.UpdateAsync(usr);
			UserDTO dto = _mapper.Map<UserDTO>(usr);
			return dto;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "LOG ERROR");
			throw;
		}
	}
}
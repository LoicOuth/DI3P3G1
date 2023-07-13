using Domain.Model;

namespace Application.Common.Interfaces.Helpers;

public interface IGetCurrentProductionHelper
{
	public Task<double> Helper(UserDTO user);

}
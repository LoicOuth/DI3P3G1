using Domain.Model;

namespace Application.Common.Interfaces.Helpers;

public interface IUpdateConsumptionsDataHelper
{
	public Task<double> Helper(UserDTO usr);
}
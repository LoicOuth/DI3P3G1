using Application.Common.Interfaces.Base;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IRepositoryUser: IRepositoryBase<User>
{
	Task<IEnumerable<User>> GetUsersAsync();

	Task<User> GetUserAsync(string id);

	Task<User> AuthenticateUser(string email, string password);
}
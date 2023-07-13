using Application.Common.Interfaces;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repository;

public class UserRepository: BaseRepository<User>, IRepositoryUser
{
	public UserRepository(
		IMongoClient mongoClient) : base(mongoClient, "User")
	{
	}

	public async Task<User> GetUserAsync(string id)
	{
		var filter = Builders<User>.Filter.Eq(f => f.Id, id);
		return await Collection.Find(filter).FirstOrDefaultAsync();
	}

	public async Task<IEnumerable<User>> GetUsersAsync() =>
		await Collection.AsQueryable().ToListAsync();

	public async Task<User> AuthenticateUser(string email, string password)
	{
		var filter = Builders<User>.Filter.Eq(f => f.Email, email);
		var res = await Collection.Find(filter).FirstOrDefaultAsync();
		if (res != null && res?.Password == password)
		{
			return res;
		}
		else if (res == null)
		{
			throw new Exception("Email not exist");
		}
		else
		{
			throw new Exception("Bad password");
		}
	}
}
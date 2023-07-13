using System.Linq.Expressions;
using Application.Common.Interfaces.Base;
using Domain.Entities.Base;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Persistence.Repository;

public class BaseRepository<T> : IRepositoryBase<T> where T : BaseEntity
{
	private const string DATABASE = "sunrise";
	private readonly IMongoClient _mongoClient;
	private readonly string _collection;



	public BaseRepository(IMongoClient mongoClient, string collection )
	{
		(_mongoClient, _collection) = (mongoClient, collection);

		if (!_mongoClient.GetDatabase(DATABASE).ListCollectionNames().ToList().Contains(collection))
			_mongoClient.GetDatabase(DATABASE).CreateCollection(collection);
	}

	protected virtual IMongoCollection<T> Collection =>
		_mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collection);

	public async Task InsertAsync(T obj) =>
		await Collection.InsertOneAsync(obj);

	public async Task UpdateAsync(T obj)
	{
		Expression<Func<T, string>> func = f => f.Id;
		var value = (string)obj.GetType().GetProperty(func.Body.ToString().Split(".")[1]).GetValue(obj, null);
		var filter = Builders<T>.Filter.Eq(func, value);

		if (obj != null)
			await Collection.ReplaceOneAsync(filter, obj);
	}

	public async Task DeleteAsync(string id) =>
		await Collection.DeleteOneAsync( f => f.Id == id);
}
using Application.Common.Interfaces;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repository;

public class IoTDataRepository: BaseRepository<IoTData>, IRepositoryIoTData
{
	public IoTDataRepository(IMongoClient mongoClient) : base(mongoClient, "IoTData")
	{
	}
	public async Task<IoTData> GetDataAsync(string id)
	{
		var filter = Builders<IoTData>.Filter.Eq(f => f.IoTHub.ConnectionDeviceId, id);
		return await Collection.Find(filter).FirstOrDefaultAsync();
	}

	public async Task<IEnumerable<IoTData>> GetAnyDataAsync() =>
		await Collection.AsQueryable().ToListAsync();
}


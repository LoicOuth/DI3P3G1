using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IRepositoryIoTData
{
	Task<IEnumerable<IoTData>> GetAnyDataAsync();

	Task<IoTData> GetDataAsync(string id);
}
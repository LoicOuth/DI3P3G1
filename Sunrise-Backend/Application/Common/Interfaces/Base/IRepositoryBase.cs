using Domain.Entities.Base;

namespace Application.Common.Interfaces.Base;

public interface IRepositoryBase<T> where T : BaseEntity
{
	Task InsertAsync(T obj);

	Task UpdateAsync(T obj);

	Task DeleteAsync(string id);
}
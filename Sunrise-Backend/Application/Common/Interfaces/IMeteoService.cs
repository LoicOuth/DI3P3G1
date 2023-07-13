using Domain.Model;

namespace Application.Common.Interfaces;

public interface IMeteoService
{
	public Task<CurrentCondition> GetCurrentMeteo(string city);
}
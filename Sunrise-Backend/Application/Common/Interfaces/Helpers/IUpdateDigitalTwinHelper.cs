namespace Application.Common.Interfaces.Helpers;

public interface IUpdateDigitalTwinHelper
{
	public Task<Unit> Helper(string id, bool enabled);
}
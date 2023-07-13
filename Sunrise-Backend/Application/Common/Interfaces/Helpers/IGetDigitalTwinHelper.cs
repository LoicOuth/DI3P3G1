namespace Application.Common.Interfaces.Helpers;

public interface IGetDigitalTwinHelper
{
	Task<bool> Helper(string Id);
}
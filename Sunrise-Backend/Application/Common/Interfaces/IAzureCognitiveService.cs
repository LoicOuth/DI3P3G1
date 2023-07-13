namespace Application.Common.Interfaces;

public interface IAzureCognitiveService
{
	Task<string> ImageToText(string fileName);
}
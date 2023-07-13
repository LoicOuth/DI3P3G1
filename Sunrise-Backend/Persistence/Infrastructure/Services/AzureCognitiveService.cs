using Application.Common.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;

namespace Persistence.Infrastructure.Services;

public class AzureCognitiveService : IAzureCognitiveService
{
	private ComputerVisionClient _client;
	private readonly string _url; 
	public AzureCognitiveService(IConfiguration config)
	{
		_client = Authenticate(config["CongnitiveService:EndPoint"], config["CongnitiveService:Key"]);
		_url = config["CongnitiveService:Host"];
	}

	public async Task<string> ImageToText(string filename)
	{
		return await ReadFileUrl( _client, $"{_url}/images/{filename}");;
	}
	
	private ComputerVisionClient Authenticate(string endpoint, string key)
	{
		ComputerVisionClient client =
			new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
				{ Endpoint = endpoint };
		return client;
	}

	private async Task<string> ReadFileUrl(ComputerVisionClient client, string urlFile)
	{
		var textHeaders = await client.ReadAsync(urlFile);
		
		string operationLocation = textHeaders.OperationLocation;
		Thread.Sleep(2000);
		
		const int numberOfCharsInOperationId = 36;
		string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

		ReadOperationResult results;

		do
		{
			results = await client.GetReadResultAsync(Guid.Parse(operationId));
		}
		while ((results.Status == OperationStatusCodes.Running ||
		        results.Status == OperationStatusCodes.NotStarted));
		string res = "";
		var textUrlFileResults = results.AnalyzeResult.ReadResults;
		foreach (ReadResult page in textUrlFileResults)
		{
			foreach (Line line in page.Lines)
			{
				res += line.Text;
			}
		}
		return res;
	}

}
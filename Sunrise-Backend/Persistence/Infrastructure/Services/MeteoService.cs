using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces;
using Domain.Model;

namespace Persistence.Infrastructure.Services;

public class MeteoService : IMeteoService
{
	private HttpClient _client;
	
	public MeteoService(IHttpClientFactory client)
	{
		_client = client.CreateClient("meteo");
	}

	public async Task<CurrentCondition> GetCurrentMeteo(string city)
	{
		using HttpResponseMessage response = await _client.GetAsync($"https://prevision-meteo.ch/services/json/{city}");
		response.EnsureSuccessStatusCode();
		string responseBody = await response.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Meteo>(responseBody).current_condition;
	}
}
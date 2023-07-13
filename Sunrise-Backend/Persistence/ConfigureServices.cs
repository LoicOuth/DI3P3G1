using System.Reflection;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Persistence.Infrastructure.Services;
using Persistence.Persistence;
using Persistence.Repository;

namespace Persistence;

public static class ConfigureServices
{
	public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddHttpClient("enedisGateway");
		
		//Configuration du service MongoDbClient
		services.AddSingleton<IMongoClient>(cfg =>
			new MongoClient(configuration["MongoDb:ConnectionString"])
		);
		services.AddScoped(c => 
			c.GetService<IMongoClient>().StartSession());
		var serviceCollection = services.AddTransient<IRepositoryUser, UserRepository>();
		var service = services.AddTransient<IRepositoryIoTData, IoTDataRepository>();
		services.AddScoped<IEnedisConsumptionService,EnedisConsumptionService>();
		services.AddScoped<IMongoClientService,MongoClientService>();
		services.AddScoped<IMeteoService, MeteoService>();
		services.AddScoped<IAzureCognitiveService, AzureCognitiveService>();
		return services;
	}
}

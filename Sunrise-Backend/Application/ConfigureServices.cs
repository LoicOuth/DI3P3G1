using Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Common.Interfaces.Helpers;
using Application.Consumption.Helpers;
using Application.Helpers;
using Application.Production.Helpers;

namespace Application;

public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		services.AddMediatR(assembly);
		services.AddValidatorsFromAssembly(assembly);
		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
		services.AddScoped<IUpdateConsumptionsDataHelper, UpdateUserConsumptionsDataHelper>();
		services.AddScoped<IGetCurrentProductionHelper, GetCurrentProductionHelper>();
		services.AddScoped<IUpdateDigitalTwinHelper, UpdateDigitalTwinHelper>();
		services.AddScoped<IGetDigitalTwinHelper, GetDigitalTwinHelper>();
		return services;
	}
}

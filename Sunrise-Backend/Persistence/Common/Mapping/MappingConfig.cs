using AutoMapper;
using Domain.Model;
using EnedisConsumptionGateway;

namespace Persistence.Common.Mapping;

public class MappingConfig : Profile
{
	public MappingConfig()
	{
		CreateMap<Interval_reading, Consumption>();
	}
}
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Domain.Model.User;

namespace Application.Common.Mapping;

public class Mapper : Profile
{
	public Mapper()
	{
		CreateMap<CreateUserModel, User>();
		CreateMap<UserDTO, User>();
		CreateMap<User, UserDTO>();
	}
}
using Moq;
using Api.Controllers;
using Application;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using Domain.Model.User;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest;

public class UserControllerTest
{
	private readonly Mock<IMediator> mockMediator;
	private readonly Mock<IMapper> mockMapper;

	public UserControllerTest()
	{
		// Configurez les mocks
		mockMediator = new Mock<IMediator>();
		mockMapper = new Mock<IMapper>();
	}
	[Fact]
	public async Task CreateUser_WhenValidInput()
	{
		var createUserModel = new CreateUserModel()
		{
			FirstName = "Yvan",
			LastName = "TRAN",
			Email = "yvan.tran@diiage.org",
			Password = "SecurePassword123",
			Address = new Address
			{
				City = "Dijon",
				Country = "France",
				StreetAddress = "1 rue de la paix",
				StreetNumber = "12",
				ZipCode = "21000"
			}
		};
		
		var command = new CreateUserCommand(createUserModel);
		
		// Configurer le Mock de Mediator et de même pour Mapper
		mockMediator.Setup(query => query.Send(command, It.IsAny<CancellationToken>()))
			.ReturnsAsync(Unit.Value);
		mockMapper.Setup(query => query.Map<CreateUserModel, User>(createUserModel))
			.Returns(new User());
		
		var userController = new UserController(mockMediator.Object, mockMapper.Object);
		var result = await userController.CreateUser(createUserModel) as OkObjectResult;

		// Assert
		// Vérifier que la méthode Send du médiateur mocké a été appelé une fois
		mockMediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
		
		// Vérifier que le StatusCode est 200
		Assert.NotNull(result);
		Assert.Equal(200, result.StatusCode);
	}
	
	[Fact]
	public async Task CreateUser_ReturnsBadRequest()
	{
		CreateUserModel invalidUserModel = null;
		var command = new CreateUserCommand(invalidUserModel);
		
		// Configurer le mock de Mediator pour lancer une ValidationException lorsque la méthode Send est appelée
		mockMediator.Setup(query => query.Send(command, It.IsAny<CancellationToken>()))
			.Throws(new ValidationException());
		
		var userController = new UserController(mockMediator.Object, mockMapper.Object);
		var result = await userController.CreateUser(invalidUserModel) as BadRequestObjectResult;

		// Assert
		// Vérifier que la méthode Send du médiateur mocké a été appelé une fois
		mockMediator.Verify(query => query.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);

		// Vérifier que le StatusCode est 400
		Assert.NotNull(result);
		Assert.Equal(400, result.StatusCode);
	}
}
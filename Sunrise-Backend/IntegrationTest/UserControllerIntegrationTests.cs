using System.Net.Http.Json;
using Domain.Entities;
using Domain.Model.User;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTest;

public class UserControllerIntegrationTests : IClassFixture<WebApplicationFactory<program>>
{
	private readonly WebApplicationFactory<program> _factory;

	public UserControllerIntegrationTests(WebApplicationFactory<program> factory)
	{
		_factory = factory;
	}
	
	[Fact]
	public async Task CreateUser_CreatesUserAndReturnsSuccessStatusCode()
	{
		// Arrange
		var client = _factory.CreateClient();
		var randomEmail = $"yvan.tran+{Guid.NewGuid()}@diiage.org";
		var createUser = new CreateUserModel()
		{
			FirstName = "Yvan",
			LastName = "TRAN",
			Email = randomEmail,
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
		
		// Act
		var response = await client.PostAsJsonAsync("/api/User", createUser);

		// Assert
		response.EnsureSuccessStatusCode(); // Le statut de réussite doit être renvoyé

		// Récupérer l'utilisateur créé et vérifier ses attributs
		var createdUserResponse = await client.GetAsync($"/api/User");
		createdUserResponse.EnsureSuccessStatusCode();
		var userList = await createdUserResponse.Content.ReadFromJsonAsync<List<User>>();

		var createdUser = userList.FirstOrDefault(u => u.Email == createUser.Email);

		Assert.NotNull(createdUser);
		Assert.Equal(createUser.FirstName, createdUser.FirstName);
		Assert.Equal(createUser.LastName, createdUser.LastName);
		Assert.Equal(createUser.Email, createdUser.Email);
		Assert.Equal(createUser.Address.City, createdUser.Address.City);
		Assert.Equal(createUser.Address.Country, createdUser.Address.Country);
		Assert.Equal(createUser.Address.StreetAddress, createdUser.Address.StreetAddress);
		Assert.Equal(createUser.Address.StreetNumber, createdUser.Address.StreetNumber);
		Assert.Equal(createUser.Address.ZipCode, createdUser.Address.ZipCode);
	}
}
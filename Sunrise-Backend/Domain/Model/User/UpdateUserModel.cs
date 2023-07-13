using Domain.Entities;

namespace Domain.Model.User;

public class UpdateUserModel
{
	
	public string FirstName { get; set; }

	public string LastName { get;set; }

	public string Email { get; set;}

	public string Password { get; set;}
	

	public string? PDM { get; set;}
	

	public string? Token { get; set;}


	public Address Address { get; set;}
}
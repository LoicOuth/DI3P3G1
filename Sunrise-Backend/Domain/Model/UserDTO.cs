namespace Domain.Model;
using System.Text.Json.Serialization;

public class UserDTO
{
	public string Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string PDM { get; set; }

	[JsonIgnore]
	public string Password { get; set; }
	
	public Entities.Consumption Consumption { get; set; }
	
	public string DeviceId { get; set; }
}
namespace Domain.Model;

public class AuthenticateResponse
{
	public UserDTO User { get; set; }
	public string Token { get; set; }


	public AuthenticateResponse(UserDTO userDto, string token)
	{
		User = userDto;
		Token = token;
	}
}
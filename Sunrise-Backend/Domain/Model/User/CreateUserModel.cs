using System.Security.Cryptography;
using System.Text;
using Domain.Entities;

namespace Domain.Model.User;

public class CreateUserModel
{
	public string FirstName { get; set; }
	public string LastName { get;set; }
	public string Email { get; set;}
	public string? PDM { get; set;}
	public string? Token { get; set;}
	private string _password;
	public Address Address { get; set;}
	public string Password
	{
		get => _password; 
		set => setPassword(value);
	}
	private void setPassword(string strData)
	{
		var message = Encoding.UTF8.GetBytes(strData);
		using (var alg = SHA512.Create())
		{
			string hex = "";

			var hashValue = alg.ComputeHash(message);
			foreach (byte x in hashValue)
			{
				hex += String.Format("{0:x2}", x);
			}

			_password = hex;
		}
	}
}


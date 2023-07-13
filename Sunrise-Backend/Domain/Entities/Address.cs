using Domain.Entities.Base;

namespace Domain.Entities;

public class Address
{
	public string City { get; set; }
	public  string ZipCode{ get; set; }
	public  string Country{ get; set; }
	public  string StreetNumber{ get; set; }
	public  string StreetAddress{ get; set; }
}
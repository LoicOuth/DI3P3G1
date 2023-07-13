using System.Linq.Expressions;
using Domain.Entities.Base;
using Domain.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class User: BaseEntity
{
	public User(){}
	public User(string fname, string lname, string email, string password, string pdm , string token, Address address) =>
		(FirstName, LastName,Email,Password,PDM,Token,Address) = (fname,lname,email,password,pdm,token,address);
	
	[BsonElement("firstname")]
	public string FirstName { get; set; }
	
	[BsonElement("lastname")]
	public string LastName { get; set;}
	
	[BsonElement("email")]
	public string Email { get;set; }
	
	[BsonElement("password")]
	public string Password { get; set;}
	
	[BsonElement("PDM")]
	public string PDM { get; set;}
	
	[BsonElement("token")]
	public string Token { get; set;}

	[BsonElement("address")]
	public Address Address { get; set;}
	
	[BsonElement("consumption")]
	public Consumption Consumption { get; set; }
	
	[BsonElement("deviceId")]
	public string DeviceId { get; set; }
	
	[BsonElement("consumptionsData")]
	public UserConsumptionsData ConsumptionsData { get; set; }
	
	[BsonElement("yesterdayCustomConsumption")]
	public List<ConsumptionInfo> YesterdayCustomConsumption { get; set; }
	
	[BsonElement("yesterdayEnedisConsumption")]
	public List<ConsumptionInfo> YesterdayEnedisConsumption { get; set; }
	
	[BsonElement("enedisUpdatedAt")]
	public DateTime EnedisUpdatedAt { get; set; }
	
	[BsonElement("geniusMode")]
	public bool GeniusMode { get; set; }
}


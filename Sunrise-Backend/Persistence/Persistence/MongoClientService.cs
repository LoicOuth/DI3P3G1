using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Persistence.Persistence;

public class MongoClientService : IMongoClientService
{
	private MongoClient _client;
	public MongoClientService()
	{

	}
	
	public string CreateSHA512(string strData)
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
			return hex;
		}
	}
}
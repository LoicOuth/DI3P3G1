using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Domain.Entities.Base;

public class BaseEntity
{
	[BsonElement("_id")]
	[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
	public virtual string Id { get; private set; }

	[BsonElement("createdAt")]
	public DateTime CreatedAt = DateTime.Now;
	
	public void SetId(string id) =>
		Id = id;
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoApi.Models;

public class TopicItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsResolved { get; set; }= false; 

    public DateTime? ResolvedDate { get; set; } = null!;
}
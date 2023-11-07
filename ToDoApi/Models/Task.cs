using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoApi.Models;

public class TaskItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;

    public decimal Priority { get; set; }

    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? StartDate { get; set; } = null!;

    public DateTime? EndDate { get; set; } = null!;
}
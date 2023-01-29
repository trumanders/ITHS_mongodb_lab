using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ITHS_mongodb_lab;


/// <summary>
/// ODM object is 
/// </summary>
public class ItemODM
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("firstName")]
    public string? FirstName { get; set; }

    [BsonElement("lastName")]
    public string? LastName { get; set; }

    [BsonElement("middleName")]
    public string? MiddleName { get; set; }

    [BsonElement("birthYear")]
    public int? BirthYear { get; set; }

    [BsonElement("deathYear")]
    public int? DeathYear { get; set; }

    [BsonElement("workedWith")]
    public string[] WorkedWith { get; set; }

    [BsonElement("isAlive")]
    public bool IsAlive { get; set; }


}

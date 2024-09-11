using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Usado como esquema para los datos de los jugadores
namespace MongoPokePlatform.Models
{
    public class Players
    {
        // Indica que "Name" se usa para identificar al jugador
        [BsonId]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("score")]
        public int Score { get; set; }
    }
}
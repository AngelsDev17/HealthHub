using HealthHub.Domain.Enums.UserManagement;
using MongoDB.Bson.Serialization.Attributes;

namespace HealthHub.Domain.Models.UserManagement;

public class Activation
{
    public bool IsActivated { get; set; }

    [BsonIgnoreIfNull, BsonIgnoreIfDefault]
    public DateTime? ActivationDate { get; set; } = null;

    [BsonIgnoreIfNull, BsonIgnoreIfDefault]
    public ActivationMethod? ActivationMethod { get; set; } = null;
}

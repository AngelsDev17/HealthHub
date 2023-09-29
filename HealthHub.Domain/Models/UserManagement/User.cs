using Angels.Packages.MongoDb.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace HealthHub.Domain.Models.UserManagement;

public class User : AuditableEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public Identification Identification { get; set; }
    public ReferencedValue Gender { get; set; }
    public string PhoneNumber { get; set; }
    public ReferencedValue City { get; set; }
    public ReferencedValue Locality { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public Activation Activation { get; set; }
}

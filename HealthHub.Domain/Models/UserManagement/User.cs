using Angels.Packages.MongoDb.Models;

namespace HealthHub.Domain.Models.UserManagement;

public class User : AuditableEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Identification Identification { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public Activation Activation { get; set; }
}

using Angels.Packages.MongoDb.Models;

namespace HealthHub.Domain.Models.UserManagement;

public class JuridicalIdentification
{
    public string Value { get; set; }
    public ReferencedValue JuridicalIdentificationType { get; set; }
}

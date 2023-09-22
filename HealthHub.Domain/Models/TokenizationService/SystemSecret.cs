using Angels.Packages.MongoDb.Models;

namespace HealthHub.Domain.Models.TokenizationService;

public class SystemSecret : AuditableEntity
{
    public byte[] Secret { get; set; }
}

using Angels.Packages.MongoDb.Models;

namespace HealthHub.Domain.Models.TokenizationService;

public class PasswordSecret : AuditableEntity
{
    public byte[] Secret { get; set; }
    public DateTime ExpirationDate { get; set; }
}

namespace HealthHub.Domain.Models.AuthService;

public class UpdateEmail
{
    public string NewEmail { get; set; } = null;
    public string UpdateEmailId { get; set; } = null;
    public int? UpdateEmailCode { get; set; } = null;
    public DateTime? ExpirationDate { get; set; } = null;
}

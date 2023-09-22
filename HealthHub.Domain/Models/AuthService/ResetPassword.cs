namespace HealthHub.Domain.Models.AuthService;

public class ResetPassword
{
    public string ResetPasswordId { get; set; } = null;
    public int? ResetPasswordCode { get; set; } = null;
    public DateTime? ExpirationDate { get; set; } = null;
}

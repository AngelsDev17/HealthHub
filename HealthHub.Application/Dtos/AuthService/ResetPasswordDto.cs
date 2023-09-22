using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.AuthService;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(10, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(18, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^([0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12})$")]
    public string ResetPasswordId { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [Range(100000, 999999, ErrorMessage = "El campo {0} no es valido.")]
    public int ResetPasswordCode { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public DateTime ResetPasswordDate { get; set; } = DateTime.Now;
}

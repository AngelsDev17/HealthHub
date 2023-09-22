using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.AuthService;

public class UpdateEmailDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string NewEmail { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^([0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12})$")]
    public string UpdateEmailId { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [Range(100000, 999999, ErrorMessage = "El campo {0} no es valido.")]
    public int UpdateEmailCode { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public DateTime UpdateEmailDate { get; set; } = DateTime.Now;
}

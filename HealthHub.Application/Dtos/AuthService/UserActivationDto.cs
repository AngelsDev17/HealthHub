using HealthHub.Application.Enums.AuthService;
using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.AuthService;

public class UserActivationDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^([0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12})$")]
    public string ActivationId { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [Range(100000, 999999, ErrorMessage = "El campo {0} no es valido.")]
    public int ActivationCode { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public ActivationMethod ActivationMethod { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public DateTime ActivationDate { get; set; } = DateTime.Now;
}

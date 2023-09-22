using HealthHub.Application.Enums.AuthService;
using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.UserManagement;

public class BasicUserActivationDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^([0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12})$")]
    public string UserId { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public DateTime ActivationDate { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [EnumDataType(typeof(ActivationMethod))]
    public ActivationMethod ActivationMethod { get; set; }
}

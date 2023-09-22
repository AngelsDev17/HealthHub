using HealthHub.Application.Enums.AuthService;
using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.Commons;

public class IdentificationDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(7, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(10, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "El campo {0} no es valido.")]
    public string Value { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [EnumDataType(typeof(IdentificationType))]
    public IdentificationType IdentificationType { get; set; }

    public string GetUserId() => $"{IdentificationType}_{Value}";
}

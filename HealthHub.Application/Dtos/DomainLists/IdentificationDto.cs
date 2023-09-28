using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.Commons;

public class IdentificationDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(7, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(10, ErrorMessage = "El campo {0} no es valido.")]
    public string Value { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public ReferencedValueDto IdentificationType { get; set; }
}

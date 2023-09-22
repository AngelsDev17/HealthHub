using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.TokenizationService;

public class TokenizedValueDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public string TokenizedValue { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^([0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12})$")]
    public string SecretId { get; set; }
}

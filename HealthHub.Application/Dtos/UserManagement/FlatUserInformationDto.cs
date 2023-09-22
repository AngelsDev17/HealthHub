using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.UserManagement;

public class FlatUserInformationDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^([0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12})$")]
    public string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(3, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(20, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(3, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(20, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Surname { get; set; }
}

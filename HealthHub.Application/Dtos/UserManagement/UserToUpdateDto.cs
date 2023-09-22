using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthHub.Application.Dtos.UserManagement;

public class UserToUpdateDto
{
    [JsonIgnore]
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

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(7, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(20, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "El campo {0} no es valido.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(5, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(30, ErrorMessage = "El campo {0} no es valido.")]
    public string Address { get; set; }
}

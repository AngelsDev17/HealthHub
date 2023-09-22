using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthHub.Application.Dtos.AuthService;

public class UserToUpdatePasswordDto
{
    [JsonIgnore]
    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(10, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(18, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$", ErrorMessage = "El campo {0} no es valido.")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(10, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(18, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$", ErrorMessage = "El campo {0} no es valido.")]
    public string NewPassword { get; set; }
}

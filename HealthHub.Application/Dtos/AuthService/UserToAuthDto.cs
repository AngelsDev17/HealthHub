using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.AuthService;

public class UserToAuthDto
{
    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(10, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(18, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$", ErrorMessage = "El campo {0} no es valido.")]
    public string Password { get; set; }
}

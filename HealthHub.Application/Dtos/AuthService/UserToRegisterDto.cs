using HealthHub.Application.Dtos.Commons;
using HealthHub.Application.Enums.AuthService;
using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.AuthService;

public class UserToRegisterDto
{
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
    public IdentificationDto Identification { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(7, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "El campo {0} no es valido.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(5, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(30, ErrorMessage = "El campo {0} no es valido.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(10, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(18, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$", ErrorMessage = "El campo {0} no es valido.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [EnumDataType(typeof(Role))]
    public Role Role { get; set; }
}

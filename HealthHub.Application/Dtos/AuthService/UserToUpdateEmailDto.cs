using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthHub.Application.Dtos.AuthService;

public class UserToUpdateEmailDto
{
    [JsonIgnore]
    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Email { get; set; }
}

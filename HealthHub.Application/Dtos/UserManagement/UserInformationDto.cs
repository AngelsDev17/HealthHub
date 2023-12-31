﻿using HealthHub.Application.Dtos.Commons;
using System.ComponentModel.DataAnnotations;

namespace HealthHub.Application.Dtos.UserManagement;

public class UserInformationDto
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

    [Range(12, 99, ErrorMessage = "El campo {0} no es valido.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public IdentificationDto Identification { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public ReferencedValueDto Gender { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(7, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(20, ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "El campo {0} no es valido.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public ReferencedValueDto City { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    public ReferencedValueDto Locality { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [MinLength(5, ErrorMessage = "El campo {0} no es valido.")]
    [MaxLength(30, ErrorMessage = "El campo {0} no es valido.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "El campo {0} no es valido.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El campo {0} no es valido.")]
    public string Email { get; set; }
}

﻿using Angels.Packages.MongoDb.Models;

namespace HealthHub.Domain.Models.AuthService;

public class AuthUser : AuditableEntity
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string SecretId { get; set; }
    public Activation Activation { get; set; } = new();
    public ResetPassword ResetPassword { get; set; } = new();
    public UpdateEmail UpdateEmail { get; set; } = new();
    public ReferencedValue Role { get; set; }
}

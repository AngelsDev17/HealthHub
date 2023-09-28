﻿using Angels.Packages.MongoDb.Models;

namespace HealthHub.Domain.Models.UserManagement;

public class Identification
{
    public string Value { get; set; }
    public ReferencedValue IdentificationType { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthHub.Application.Dtos.Commons;

public class ReferencedValueDto
{
    [Required]
    public string Id { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ParentId { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Value { get; set; } = null;
}

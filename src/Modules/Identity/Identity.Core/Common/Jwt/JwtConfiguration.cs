using System.ComponentModel.DataAnnotations;

namespace Identity.Core.Common.Jwt;

public sealed class JwtConfiguration
{
    public const string SectionName = "Jwt";

    [Required]
    public string TokenSigningKey { get; init; } = null!;
    
    [Required]
    [Range(900, int.MaxValue)]
    public int TokenLifeTimeInSeconds { get; init; }
}
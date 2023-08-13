using System.ComponentModel.DataAnnotations;

namespace Identity.Core.Common.Jwt;

internal sealed class JwtConfiguration
{
    public const string SectionName = "Jwt";

    [Required]
    public string TokenSigningKey { get; init; } = null!;
    
    [Required]
    public int TokenLifeTimeInSeconds { get; init; } = (int)TimeSpan.FromSeconds(3600).TotalSeconds;
}
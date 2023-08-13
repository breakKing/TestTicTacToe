using Identity.Core.Common.Identity.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Core.Common.Identity;

internal sealed class IdentityContext : IdentityDbContext<User, Role, Guid>
{
    /// <inheritdoc />
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }
}
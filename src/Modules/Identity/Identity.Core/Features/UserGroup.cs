using Common.Api;

namespace Identity.Core.Features;

public sealed class UserGroup : EndpointGroupBase
{
    /// <inheritdoc />
    public UserGroup() : base("User", "user")
    {
    }
}
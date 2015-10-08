using System;
using RevStack.Pattern;

namespace RevStack.Identity
{
    public interface IIdentityUserClaim<TKey> : IEntity<TKey>
    {
        TKey UserId { get; set; }
        string ClaimType { get; set; }
        string ClaimValue { get; set; }
    }
}

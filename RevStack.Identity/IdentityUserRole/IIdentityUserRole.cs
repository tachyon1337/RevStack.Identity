using System;
using RevStack.Pattern;

namespace RevStack.Identity
{
    public interface IIdentityUserRole<TKey> : IEntity<TKey>
    {
        TKey UserId { get; set; }
        string RoleId { get; set; }
    }
}

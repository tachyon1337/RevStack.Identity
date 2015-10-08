using System;
using RevStack.Pattern;

namespace RevStack.Identity
{
    public interface IIdentityUserLogin<TKey> : IEntity<TKey>
    {
        string LoginProvider { get; set; }
        string ProviderKey { get; set; }
        TKey UserId { get; set; }
    }
}

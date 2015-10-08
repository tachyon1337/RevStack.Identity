using System;

namespace RevStack.Identity
{
    public class IdentityUserLogin<TKey> : IIdentityUserLogin<TKey>
    {
        public TKey Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public TKey UserId { get; set; }
    }
}

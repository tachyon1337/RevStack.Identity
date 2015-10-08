using System;

namespace RevStack.Identity
{
    public class IdentityUserClaim<TKey> : IIdentityUserClaim<TKey>
    {
        public TKey Id { get; set; }
        public TKey UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}

using System;

namespace RevStack.Identity
{
    public class IdentityUserRole<TKey> : IIdentityUserRole<TKey>
    {
        public TKey Id { get; set; }
        public TKey UserId { get; set; }
        public string RoleId { get; set; }
    }
}

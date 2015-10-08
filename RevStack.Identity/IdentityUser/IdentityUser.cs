using System;


namespace RevStack.Identity
{
    public class IdentityUser<TKey> : IIdentityUser<TKey>
    {
        public TKey Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool IsLockoutEnabled { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTimeOffset? LockoutEndDate { get; set; }

        public IdentityUser() { }
        public IdentityUser(string userName)
        {
            UserName = userName;
        }
    }
}

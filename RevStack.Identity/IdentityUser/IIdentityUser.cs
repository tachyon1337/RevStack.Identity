using System;
using Microsoft.AspNet.Identity;
using RevStack.Pattern;

namespace RevStack.Identity
{
    public interface IIdentityUser<TKey> : IEntity<TKey>,IUser<TKey>
    {
        new TKey Id { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        string PasswordHash { get; set; }
        string SecurityStamp { get; set; }
        bool IsLockoutEnabled { get; set; }
        bool IsTwoFactorEnabled { get; set; }
        int AccessFailedCount { get; set; }
        DateTimeOffset? LockoutEndDate { get; set; }
    }
}

using System;
using Microsoft.AspNet.Identity;

namespace RevStack.Identity
{
    public interface IIdentityUserStore<TUser, in TKey> : IUserStore<TUser, TKey>, IUserLoginStore<TUser, TKey>, IUserClaimStore<TUser, TKey>,
       IUserRoleStore<TUser, TKey>, IUserPasswordStore<TUser, TKey>, IUserSecurityStampStore<TUser, TKey>, IUserEmailStore<TUser, TKey>,
       IUserPhoneNumberStore<TUser, TKey>, IUserLockoutStore<TUser, TKey>, IUserTwoFactorStore<TUser, TKey>
       where TUser : class,IIdentityUser<TKey>
    {

    }
}

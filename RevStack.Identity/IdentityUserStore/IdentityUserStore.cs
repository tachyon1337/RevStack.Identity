using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using RevStack.Pattern;
using RevStack.Mvc;

namespace RevStack.Identity
{
    public class IdentityUserStore<TUser,TKey> : IIdentityUserStore<TUser,TKey> where TUser: class,IIdentityUser<TKey>
    {
        #region "Private Fields"
        private readonly IIdentityUserLogin<TKey> _identityUserLogin;
        private readonly IIdentityUserClaim<TKey> _identityUserClaim;
        private readonly IIdentityUserRole<TKey> _identityUserRole;

        private readonly IRepository<IIdentityUser<TKey>, TKey> _userRepository;
        private readonly IRepository<IIdentityUserLogin<TKey>, TKey> _userLoginRepository;
        private readonly IRepository<IIdentityUserClaim<TKey>, TKey> _userClaimRepository;
        private readonly IRepository<IIdentityUserRole<TKey>, TKey> _userRoleRepository;
        private readonly IRepository<IIdentityRole, string> _roleRepository;

        private readonly IUnitOfWork<IIdentityUser<TKey>, TKey> _userUnitOfWork;
        private readonly IUnitOfWork<IIdentityUserLogin<TKey>, TKey> _userLoginUnitOfWork;
        private readonly IUnitOfWork<IIdentityUserClaim<TKey>, TKey> _userClaimUnitOfWork;
        private readonly IUnitOfWork<IIdentityUserRole<TKey>, TKey> _userRoleUnitOfWork;
        private readonly IUnitOfWork<IIdentityRole, string> _roleUnitOfWork;
        #endregion

        #region "Constructor"
        public IdentityUserStore(IRepository<IIdentityUser<TKey>, TKey> userRepository, 
            IRepository<IIdentityUserLogin<TKey>, TKey> userLoginRepository,
            IRepository<IIdentityUserClaim<TKey>, TKey> userClaimRepository, 
            IRepository<IIdentityUserRole<TKey>, TKey> userRoleRepository,
            IRepository<IIdentityRole, string> roleRepository,
            IUnitOfWork<IIdentityUser<TKey>, TKey> userUnitOfWork, 
            IUnitOfWork<IIdentityUserLogin<TKey>, TKey> userLoginUnitOfWork,
            IUnitOfWork<IIdentityUserClaim<TKey>, TKey> userClaimUnitOfWork, 
            IUnitOfWork<IIdentityUserRole<TKey>, TKey> userRoleUnitOfWork,
            IUnitOfWork<IIdentityRole, string> roleUnitOfWork,
            IIdentityUserLogin<TKey> identityUserLogin,
            IIdentityUserClaim<TKey> identityUserClaim, 
            IIdentityUserRole<TKey> identityUserRole

            )
        {
            _userRepository = userRepository;
            _userLoginRepository = userLoginRepository;
            _userClaimRepository = userClaimRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;

            _userUnitOfWork = userUnitOfWork;
            _userLoginUnitOfWork = userLoginUnitOfWork;
            _userClaimUnitOfWork = userClaimUnitOfWork;
            _userRoleUnitOfWork = userRoleUnitOfWork;
            _roleUnitOfWork = roleUnitOfWork;

            _identityUserLogin = identityUserLogin;
            _identityUserClaim = identityUserClaim;
            _identityUserRole = identityUserRole;
        }
        #endregion

        #region "Public Members"
        public virtual Task CreateAsync(TUser user)
        {
            _create(user);
            return Task.FromResult(true);
        }

        public virtual Task DeleteAsync(TUser user)
        {
            _delete(user);
            return Task.FromResult(true);
        }

        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {
            return Task.FromResult(_findById(userId));
        }

        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(_findByName(userName));
        }

        public virtual Task UpdateAsync(TUser user)
        {
            _update(user);
            return Task.FromResult(true);
        }

        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            _addLogin(user, login);
            return Task.FromResult(true);
        }

        public virtual Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            _removeLogin(user, login);
            return Task.FromResult(true);
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            return Task.FromResult(_getLogins(user));
        }

        public virtual Task<TUser> FindAsync(UserLoginInfo login)
        {
            return Task.FromResult(_find(login));
        }

        public virtual Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            return Task.FromResult(_getClaims(user));
        }

        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            _addClaim(user, claim);
            return Task.FromResult(true);
        }

        public virtual Task RemoveClaimAsync(TUser user, Claim claim)
        {
            _removeClaim(user, claim);
            return Task.FromResult(true);
        }

        public virtual Task AddToRoleAsync(TUser user, string roleName)
        {
            _addToRole(user, roleName);
            return Task.FromResult(true);
        }

        public virtual Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            _removeFromRole(user, roleName);
            return Task.FromResult(true);
        }

        public virtual Task<IList<string>> GetRolesAsync(TUser user)
        {
            return Task.FromResult(_getRoles(user));
        }

        public virtual Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            return Task.FromResult(_isInRole(user, roleName));
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            _setPasswordHash(user, passwordHash);
            return Task.FromResult(true);
        }

        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(_getPasswordHash(user));
        }

        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(_hasPassword(user));
        }

        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {
            _setSecurityStamp(user, stamp);
            return Task.FromResult(true);
        }

        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(_getSecurityStamp(user));
        }

        public virtual Task SetEmailAsync(TUser user, string email)
        {
            _setEmail(user, email);
            return Task.FromResult(true);
        }

        public virtual Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(_getEmail(user));
        }

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return Task.FromResult(_getEmailConfirmed(user));
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            _setEmailConfirmed(user, confirmed);
            return Task.FromResult(true);
        }

        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(_findByEmail(email));
        }

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            _setPhoneNumber(user, phoneNumber);
            return Task.FromResult(true);
        }

        public virtual Task<string> GetPhoneNumberAsync(TUser user)
        {
            return Task.FromResult(_getPhoneNumber(user));
        }

        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            return Task.FromResult(_getPhoneNumberConfirmed(user));
        }

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            _setPhoneNumberConfirmed(user, confirmed);
            return Task.FromResult(true);
        }

        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return Task.FromResult(_getLockoutEndDate(user));
        }

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            _setLockoutEndDate(user, lockoutEnd);
            return Task.FromResult(true);
        }

        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(_incrementAccessFailedCount(user));
        }

        public virtual Task ResetAccessFailedCountAsync(TUser user)
        {
            _resetAccessFailedCount(user);
            return Task.FromResult(true);
        }

        public virtual Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(_getAccessFailedCount(user));
        }

        public virtual Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(_getLockoutEnabled(user));
        }

        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            _setLockoutEnabled(user, enabled);
            return Task.FromResult(true);
        }

        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            _setTwoFactorEnabled(user, enabled);
            return Task.FromResult(true);
        }

        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(_getTwoFactorEnabled(user));
        }

        #endregion

        #region "Private Members"
        private void _create(TUser user)
        {
            _userRepository.Add(user);
            _userUnitOfWork.Commit();
        }

        private void _delete(TUser user)
        {
            _userRepository.Delete(user);
            _userUnitOfWork.Commit();
        }

        private TUser _findById(TKey userId)
        {
            return (TUser)_userRepository.Find(x => x.Compare(x.Id, userId)).ToSingleOrDefault();
        }

        private TUser _findByName(string userName)
        {
            return (TUser)_userRepository.Find(x => x.UserName == userName).ToSingleOrDefault();
        }

        private void _update(TUser user)
        {
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private void _addLogin(TUser user, UserLoginInfo login)
        {
            _identityUserLogin.Id = user.Id;
            _identityUserLogin.LoginProvider = login.LoginProvider;
            _identityUserLogin.ProviderKey = login.ProviderKey;

            _userLoginRepository.Add(_identityUserLogin);
            _userLoginUnitOfWork.Commit();

        }

        private void _removeLogin(TUser user, UserLoginInfo login)
        {
            var identityLogin = _userLoginRepository.Find(x => x.Compare(x.UserId, user.Id) && x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey).ToSingleOrDefault();
            if(identityLogin !=null)
            {
                _userLoginRepository.Delete(identityLogin);
                _userLoginUnitOfWork.Commit();
            }
        }

        private IList<UserLoginInfo> _getLogins(TUser user)
        {
            var logins = _userLoginRepository.Find(x => x.Compare(x.UserId, user.Id));
            if(logins.Any())
            {
                return logins.Select(x => new UserLoginInfo(x.LoginProvider, x.LoginProvider)).ToList();
            }
            else
            {
                return new List<UserLoginInfo>();
            }
        }

        private TUser _find(UserLoginInfo login)
        {
            TUser user = null;
            var userLogin = _userLoginRepository.Find(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey).ToSingleOrDefault();
            if(userLogin !=null)
            {
                user = (TUser)_userRepository.Find(x => x.Compare(x.Id, userLogin.UserId)).ToSingleOrDefault();
            }
            return user;
        }

        private IList<Claim> _getClaims(TUser user)
        {
            var userClaims = _userClaimRepository.Find(x => x.Compare(x.UserId, user.Id));
            if(userClaims.Any())
            {
                return userClaims.Select(x => new Claim(x.ClaimType, x.ClaimType)).ToList();
            }
            else
            {
                return new List<Claim>();
            }
        }

        private void _addClaim(TUser user, Claim claim)
        {
            _identityUserClaim.UserId = user.Id;
            _identityUserClaim.ClaimType = claim.Type;
            _identityUserClaim.ClaimValue = claim.Value;

            _userClaimRepository.Add(_identityUserClaim);
            _userClaimUnitOfWork.Commit();
        }

        private void _removeClaim(TUser user, Claim claim)
        {
            var userClaims = _userClaimRepository.Find(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value && x.Compare(x.UserId, user.Id));
            if(userClaims.Any())
            {
                var userClaim = userClaims.ToSingleOrDefault();
                _userClaimRepository.Delete(userClaim);
                _userClaimUnitOfWork.Commit();
            }
        }

        private void _addToRole(TUser user, string roleName)
        {
            var role = _roleRepository.Find(x => x.Name == roleName).ToSingleOrDefault();
            if(role!=null)
            {
                _identityUserRole.UserId = user.Id;
                _identityUserRole.RoleId = role.Id;

                _userRoleRepository.Add(_identityUserRole);
                _userRoleUnitOfWork.Commit();
            }
        }

        private void _removeFromRole(TUser user, string roleName)
        {
            var role = _roleRepository.Find(x => x.Name == roleName).ToSingleOrDefault();
            if (role != null)
            {
                var userRole = _userRoleRepository.Find(x => x.Compare(x.UserId, user.Id) && x.RoleId == role.Id).ToSingleOrDefault();
                if(userRole !=null)
                {
                    _userRoleRepository.Delete(userRole);
                    _userRoleUnitOfWork.Commit();
                }
            }
        }

        private IList<string> _getRoles(TUser user)
        {
            var roles = _userRoleRepository.Find(x => x.Compare(x.UserId, user.Id));
            if(roles.Any())
            {
                return _listOfRoles(roles);
            }
            else
            {
                return new List<string>();
            }
        }

        private List<string> _listOfRoles(IQueryable<IIdentityUserRole<TKey>> roles)
        {
            return roles
              .Join(_roleRepository.Get(), x => x.RoleId, r => r.Id, (x, r) => new { x, r })
              .Select(result => result.r.Name).ToList();
        }

        private bool _isInRole(TUser user, string roleName)
        {
            var roles = _userRoleRepository.Find(x => x.Compare(x.UserId, user.Id));
            if (roles.Any())
            {
                return _listOfRoles(roles).Select(x => x == roleName).SingleOrDefault();
            }
            else
            {
                return false;
            }
        }

        private void _setPasswordHash(TUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private string _getPasswordHash(TUser user)
        {
            return user.PasswordHash;
        }

        private bool _hasPassword(TUser user)
        {
            return (user.PasswordHash != null);
        }

        private void _setSecurityStamp(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private string _getSecurityStamp(TUser user)
        {
            return user.SecurityStamp;
        }

        private void _setEmail(TUser user, string email)
        {
            user.Email = email;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private string _getEmail(TUser user)
        {
            return user.Email;
        }

        private bool _getEmailConfirmed(TUser user)
        {
            return user.EmailConfirmed;
        }

        private void _setEmailConfirmed(TUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private TUser _findByEmail(string email)
        {
            return (TUser)_userRepository.Find(x => x.Email.ToLower() == email.ToLower()).ToSingleOrDefault();
;        }

        private void _setPhoneNumber(TUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private string _getPhoneNumber(TUser user)
        {
            return user.PhoneNumber;
        }

        private bool _getPhoneNumberConfirmed(TUser user)
        {
            return user.PhoneNumberConfirmed;
        }

        private void _setPhoneNumberConfirmed(TUser user, bool confirmed)
        {
            user.PhoneNumberConfirmed = confirmed;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private DateTimeOffset _getLockoutEndDate(TUser user)
        {
            if (user.LockoutEndDate == null) return new DateTimeOffset(DateTime.Now.AddDays(-1));
            return user.LockoutEndDate.Value;
        }

        private void _setLockoutEndDate(TUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDate = lockoutEnd;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private int _incrementAccessFailedCount(TUser user)
        {
            int count= user.AccessFailedCount++;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
            return count;
        }

        private void _resetAccessFailedCount(TUser user)
        {
            user.AccessFailedCount = 0;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private int _getAccessFailedCount(TUser user)
        {
            return user.AccessFailedCount;
        }

        private bool _getLockoutEnabled(TUser user)
        {
            return user.IsLockoutEnabled;
        }

        private void _setLockoutEnabled(TUser user, bool enabled)
        {
            user.IsLockoutEnabled = enabled;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private void _setTwoFactorEnabled(TUser user, bool enabled)
        {
            user.IsTwoFactorEnabled = enabled;
            _userRepository.Update(user);
            _userUnitOfWork.Commit();
        }

        private bool _getTwoFactorEnabled(TUser user)
        {
            return user.IsTwoFactorEnabled;
        }
        #endregion


        #region IDisposable Support
        private void disposeWork()
        {
            if (_userUnitOfWork != null)
            {
                _userUnitOfWork.Dispose();
            }
            if (_userLoginUnitOfWork != null)
            {
                _userLoginUnitOfWork.Dispose();
            }
            if (_userClaimUnitOfWork != null)
            {
                _userClaimUnitOfWork.Dispose();
            }
            if (_userRoleUnitOfWork != null)
            {
                _userRoleUnitOfWork.Dispose();
            }
            if (_roleUnitOfWork != null)
            {
                _roleUnitOfWork.Dispose();
            }
        }
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    disposeWork();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}

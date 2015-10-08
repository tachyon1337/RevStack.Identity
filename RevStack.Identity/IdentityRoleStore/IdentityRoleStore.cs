using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RevStack.Pattern;
using RevStack.Mvc;

namespace RevStack.Identity
{
    public class IdentityRoleStore<TRole> : IRoleStore<TRole> where TRole :class,IIdentityRole
    {
        #region "Private Fields"
        private readonly IRepository<IIdentityRole, string> _roleRepository;
        private readonly IUnitOfWork<IIdentityRole, string> _roleUnitOfWork;
        #endregion

        #region "Constructor"
        public IdentityRoleStore(IRepository<IIdentityRole, string> roleRepository, IUnitOfWork<IIdentityRole, string> roleUnitOfWork)
        {
            _roleRepository = roleRepository;
            _roleUnitOfWork = roleUnitOfWork;
        }
        #endregion

        #region "Public Members"
        public Task CreateAsync(TRole role)
        {
            _create(role);
            return Task.FromResult(true);
        }

        public Task DeleteAsync(TRole role)
        {
            _delete(role);
            return Task.FromResult(true);
        }

        public Task<TRole> FindByIdAsync(string roleId)
        {
            return Task.FromResult(_findById(roleId));
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult(_findByName(roleName));
        }

        public Task UpdateAsync(TRole role)
        {
            _update(role);
            return Task.FromResult(true);
        }
        #endregion


        #region "Private Members"
        private void _create(TRole role)
        {
            _roleRepository.Add(role);
            _roleUnitOfWork.Commit();
        }

        private void _delete(TRole role)
        {
            _roleRepository.Delete(role);
            _roleUnitOfWork.Commit();
        }

        private TRole _findById(string roleId)
        {
            return (TRole)_roleRepository.Find(x => x.Id == roleId).ToSingleOrDefault();
        }

        private TRole _findByName(string roleName)
        {
            return (TRole)_roleRepository.Find(x => x.Name==roleName).ToSingleOrDefault();
        }

        private void _update(TRole role)
        {
            _roleRepository.Update(role);
            _roleUnitOfWork.Commit();
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

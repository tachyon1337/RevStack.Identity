using System;
using RevStack.Pattern;
using Microsoft.AspNet.Identity;

namespace RevStack.Identity
{
    public interface IIdentityRole : IRole<string>, IEntity<string>
    {
       new string Id { get; set; }
    }
}

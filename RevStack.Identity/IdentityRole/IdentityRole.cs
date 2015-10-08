using System;


namespace RevStack.Identity
{
    public class IdentityRole : IIdentityRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace Blazor4.Data.CustomProvider
{
    public class ApplicationUser : IdentityUser
    {
        public bool AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string PasswordSalt { get; internal set; }
        public int BusinessEntityId { get; internal set; }

    }
}


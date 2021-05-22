
using System.Collections.ObjectModel;

namespace Initial_Clean_Architecture.Application.Domain.Constants
{
    public class RolesConst
    {
        /// <summary>
        /// List of all roles of all roles the system in lower case
        /// </summary>
        public static readonly ReadOnlyCollection<string> AllRoles = new ReadOnlyCollection<string>(new[]
        {
              SuperAdmin.ToLower(),
              Admin.ToLower()
        });

        public const string SuperAdmin = nameof(SuperAdmin);
        public const string Admin = nameof(Admin);
    }
}

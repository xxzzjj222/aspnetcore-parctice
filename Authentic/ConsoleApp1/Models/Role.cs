using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Role
    {
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }
        public virtual ICollection<UserRole> Users { get; } = new List<UserRole>();

        public Role() { }
        public Role(string roleName)
        {
            RoleName = roleName;
            NormalizedRoleName = roleName.ToUpper();
        }
    }
}

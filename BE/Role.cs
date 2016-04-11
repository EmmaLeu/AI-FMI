using System.Collections.Generic;

namespace BE
{
    public partial class Role
    {
        public Role()
        {
            this.Permissions = new List<Permission>();
            this.Users = new List<User>();
        }

        public int RoleID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

}

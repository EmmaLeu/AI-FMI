using System.Collections.Generic;

namespace BE
{
    public partial class Permission
    {
        public Permission()
        {
            this.Roles = new List<Role>();
        }

        public int PermissionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Role> Roles { get; set; }


    }
}

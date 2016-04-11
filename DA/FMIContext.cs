using BE;
using DA.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public partial class FMIContext : DbContext
    {
        public FMIContext()
            : base("name=FMIContext")
        {
        }

        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Upload> Uploads { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Award> Awards { get; set; }
        public virtual DbSet<Collaborator> Collaborators { get; set; }
        public virtual DbSet<SoftwareDataset> Software { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EducationMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new NewsMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new PublicationMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UploadMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new AwardMap());
            modelBuilder.Configurations.Add(new CollaboratorMap());
            modelBuilder.Configurations.Add(new SoftwareMap());






        }
    }
}

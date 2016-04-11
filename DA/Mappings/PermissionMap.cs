using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Mappings
{
    public class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.PermissionID);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Permission");
            this.Property(t => t.PermissionID).HasColumnName("PermissionID");
            this.Property(t => t.Title).HasColumnName("Tiltle");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasMany(t => t.Roles)
                .WithMany(t => t.Permissions)
                .Map(m =>
                {
                    m.ToTable("RolePermission");
                    m.MapLeftKey("PermissionID");
                    m.MapRightKey("RoleID");
                });


        }
    }
}

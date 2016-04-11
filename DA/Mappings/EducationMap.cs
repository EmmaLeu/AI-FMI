using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Mappings
{
    public class EducationMap: EntityTypeConfiguration<Education>
    {
        public EducationMap()
        {
            //Primary Key
            this.HasKey(t => t.EducationID);

            //Properties & column mappings
            this.ToTable("Education");

            this.Property(t => t.EducationID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("EducationID");

            this.Property(t => t.UserID)
                .IsRequired()
                .HasColumnName("UserID");

            this.Property(t => t.StartDate)
                .IsRequired()
                .HasColumnName("StartDate");

            this.Property(t => t.EndDate)
                .HasColumnName("EndDate");

            this.Property(t => t.Position)
                .IsRequired()
                .HasColumnName("Position");

            this.Property(t => t.Institution)
                .IsRequired()
                .HasColumnName("Institution");

            this.Property(t => t.Activities)
                .IsRequired()
                .HasColumnName("Activities");

            this.Property(t => t.CreationDate)
                .IsRequired()
                .HasColumnName("CreationDate");

            //Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.EducationList)
                .HasForeignKey(d => d.UserID);
        }
    }
}

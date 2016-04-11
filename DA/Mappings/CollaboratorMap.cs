using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Mappings
{
    public class CollaboratorMap:EntityTypeConfiguration<Collaborator>
    {
        public CollaboratorMap()
        {
            //Primary Key
            this.HasKey(t => t.CollaboratorID);

            //Properties & column mappings
            this.ToTable("Collaborator");

            this.Property(t => t.CollaboratorID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("CollaboratorID");

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasColumnName("FirstName");

            this.Property(t => t.LastName)
                .IsRequired()
                .HasColumnName("LastName");

            this.Property(t => t.CreationDate)
                .IsRequired()
                .HasColumnName("CreationDate");

            //Relationships
            this.HasOptional(t => t.ProfilePicture)
                .WithMany(t => t.Collaborators)
                .HasForeignKey(d => d.ImageID);
        }
    }
}

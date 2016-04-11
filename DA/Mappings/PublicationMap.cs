using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Mappings
{
    public class PublicationMap : EntityTypeConfiguration<Publication>
    {
        public PublicationMap()
        {
            //Primary Key
            this.HasKey(t => t.PublicationID);

            //Properties & column mappings
            this.ToTable("Publication");

            this.Property(t => t.PublicationID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("PublicationID");

            this.Property(t => t.UserID)
                .IsRequired()
                .HasColumnName("UserID");

            this.Property(t => t.UploadID)
                .HasColumnName("UploadID");

            this.Property(t => t.Category)
                .IsRequired()
                .HasColumnName("Category");

            this.Property(t => t.Title)
                .HasColumnName("Title");

            this.Property(t => t.Authors)
                .IsRequired()
                .HasColumnName("Authors");

            this.Property(t => t.PublicationYear)
                .IsRequired();

            this.Property(t => t.Pages)
                .HasColumnName("Pages");

            this.Property(t => t.Abstract)
                .HasColumnName("Abstract");

            this.Property(t => t.Link)
                .HasColumnName("Link");

            //Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Publications)
                .HasForeignKey(d => d.UserID);

            this.HasOptional(t => t.Upload)
                .WithMany(t => t.Publications)
                .HasForeignKey(d => d.UploadID);

         }
    }
}

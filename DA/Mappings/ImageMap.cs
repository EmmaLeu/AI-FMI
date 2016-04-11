using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;
namespace DA.Mappings
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            // Primary Key
            this.HasKey(t => t.ImageID);

            //Properties
            this.Property(t => t.ImageID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FilePath)
                .IsRequired();

            this.Property(t => t.FileSize)
                .IsRequired();

            this.Property(t => t.CreationDate)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Image");
            this.Property(t => t.ImageID).HasColumnName("ImageID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.FilePath).HasColumnName("FilePath");
            this.Property(t => t.FileSize).HasColumnName("FileSize");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.SoftwareDatasetID).HasColumnName("SoftwareDatasetID");
            this.Property(t => t.PublicationID).HasColumnName("PublicationID");

            this.HasOptional(t => t.Publication)
                .WithMany(t => t.Images)
                .HasForeignKey(d => d.PublicationID);

            this.HasOptional(t => t.SoftwareDataset)
                .WithMany(t => t.Images)
                .HasForeignKey(d => d.SoftwareDatasetID);
        }

    }
}

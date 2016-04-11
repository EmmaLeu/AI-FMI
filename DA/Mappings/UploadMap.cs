using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Mappings
{
    public class UploadMap : EntityTypeConfiguration<Upload>
    {
        public UploadMap()
        {
            //Primary Key
            this.HasKey(t => t.UploadID);

            //Properties & column mappings
            this.ToTable("Upload");

            this.Property(t => t.UploadID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("UploadID");

            this.Property(t => t.FileName)
                .IsRequired()
                .HasColumnName("FileName");

            this.Property(t => t.FileSize)
                .IsRequired()
                .HasColumnName("FileSize");

            this.Property(t => t.FilePath)
                .IsRequired()
                .HasColumnName("FilePath");

            this.Property(t => t.CreationDate)
                .IsRequired()
                .HasColumnName("CreationDate");

            this.Property(t => t.Hash)
                .HasColumnName("Hash");

        }
    }
}

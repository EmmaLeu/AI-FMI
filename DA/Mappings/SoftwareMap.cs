using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Mappings
{
    public class SoftwareMap : EntityTypeConfiguration<SoftwareDataset>
    {
        public SoftwareMap()
        {
            //Primary Key
            this.HasKey(t => t.ID);

            //Properties & column mappings
            this.ToTable("SoftwareDataset");

            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("ID");

            this.Property(t => t.UserID)
                .IsRequired()
                .HasColumnName("UserID");

            this.Property(t => t.Title)
                .IsRequired()
                .HasColumnName("Title");

            this.Property(t => t.Description)
                .HasColumnName("Description");

            this.Property(t => t.CreationDate)
                .IsRequired()
                .HasColumnName("CreationDate");

            this.Property(t => t.Type)
                .IsRequired()
                .HasColumnName("Type");

            this.Property(t => t.Authors)
                .HasColumnName("Authors");

            this.Property(t => t.CounterDownloads)
                .HasColumnName("CounterDownloads");

            this.Property(t => t.CounterLinkViews)
                .HasColumnName("CounterLinkViews");

            this.Property(t => t.UploadID)
                .HasColumnName("UploadID");

            //Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.SoftwareDatasets)
                .HasForeignKey(d => d.UserID);

            this.HasOptional(t => t.Upload)
                .WithMany(t => t.SoftwareDatasets)
                .HasForeignKey(d => d.UploadID);
        }
    }
}
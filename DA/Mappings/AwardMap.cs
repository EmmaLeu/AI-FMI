using System;
using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Mappings
{
    public class AwardMap : EntityTypeConfiguration<Award>
    {
        public AwardMap()
        {
            //Primary Key
            this.HasKey(t => t.AwardID);

            //Properties & column mappings
            this.ToTable("Award");

            this.Property(t => t.AwardID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("AwardID");

            this.Property(t => t.UserID)
                .IsRequired()
                .HasColumnName("UserID");

            this.Property(t => t.Title)
                .IsRequired()
                .HasColumnName("Title");

            this.Property(t => t.CreationDate)
                .IsRequired()
                .HasColumnName("CreationDate");

            this.Property(t => t.Description)
                .HasColumnName("Description");

            //Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Awards)
                .HasForeignKey(d => d.UserID);
        }
    }
}

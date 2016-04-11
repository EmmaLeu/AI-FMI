using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Mappings
{
    public class NewsMap: EntityTypeConfiguration<News>
    {
        public NewsMap()
        {
            //Primary Key
            this.HasKey(t => t.NewsID);

            //Properties & column mappings
            this.ToTable("News");

            this.Property(t => t.NewsID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("NewsID");

            this.Property(t => t.UserID)
                .IsRequired()
                .HasColumnName("UserID");

            this.Property(t => t.Title)
                .HasColumnName("Title");

            this.Property(t => t.Description)
                .IsRequired()
                .HasColumnName("Description");

            this.Property(t => t.Link)
                .HasColumnName("Link");

            //Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.NewsList)
                .HasForeignKey(d => d.UserID);
                
        }
    }
}

using System.Data.Entity.ModelConfiguration;
using BE;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            //Primary Key
            this.HasKey(t => t.UserID);

            //Proprieties
            this.Property(t => t.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LoginEmail)
                .IsRequired()
                .HasMaxLength(254);

            this.Property(t => t.PasswordHash)
                .IsRequired();

            this.Property(t => t.CreationDate)
                .IsRequired();

            this.Property(t => t.IsDeleted)
                .IsRequired();

            this.Property(t => t.Gender)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.LoginEmail).HasColumnName("LoginEmail");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Rank).HasColumnName("Rank");
            this.Property(t => t.InterestAreas).HasColumnName("InterestAreas");
            this.Property(t => t.ContactEmail).HasColumnName("ContactEmail");
            this.Property(t => t.CurrentInsitution).HasColumnName("CurrentInstitution");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.Nationality).HasColumnName("Nationality");
            this.Property(t => t.ImageID).HasColumnName("ImageID");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            //Relationships

            this.HasOptional(t => t.ProfilePicture)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.ImageID)
                .WillCascadeOnDelete(false);

        }
    }
}
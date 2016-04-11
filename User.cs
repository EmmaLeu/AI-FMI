using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public partial class User
    {
        public User()
        {
            this.Publications = new List<Publication>();
            this.EducationList = new List<Education>();
            this.Roles = new List<Role>();
            this.NewsList = new List<News>();
        }

        public int UserID { get; set; }
        public string LoginEmail { get; set; }
        public string PasswordHash { get; set; }
        public int Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Gender { get; set; }
        public bool IsDeleted { get; set; }
        public string Nationality { get; set; }
        public int? ImageID { get; set; }
        public string Title { get; set; }
        public string Rank { get; set; }
        public string InterestAreas { get; set; }
        public string ContactEmail { get; set; }
        public string CurrentInsitution { get; set; }

        public virtual ICollection<Education> EducationList { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<News> NewsList { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual Image ProfilePicture { get; set; }

    }

}
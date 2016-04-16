using System;
using System.Collections.Generic;

namespace BE
{
    public partial class Collaborator
    {

        public int CollaboratorID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? ImageID { get; set; }

        public string Title { get; set; }

        public string Institution { get; set; }

        public virtual Image ProfilePicture { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}

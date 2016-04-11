using System;
using System.Collections.Generic;

namespace BE
{   
    public partial class Image
    {
        public Image()
        {
            this.Users = new List<User>();
            this.Collaborators = new List<Collaborator>();
        }

        public int ImageID { get; set; }

        public int? UserID { get; set; }

        public string FilePath { get; set; }

        public long FileSize { get; set; }

        public DateTime CreationDate { get; set; }

        public string ImageType { get; set; }

        public string Name { get; set; }

        public int? PublicationID { get; set; }

        public int? SoftwareDatasetID { get; set; }

        public virtual Publication Publication { get; set; }

        public virtual SoftwareDataset SoftwareDataset { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Collaborator> Collaborators { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BE
{
    public partial class Upload
    {
        public Upload()
        {
            this.Publications = new List<Publication>();
            this.SoftwareDatasets = new List<SoftwareDataset>();
        }

        public int UploadID { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string FilePath { get; set; }

        public DateTime CreationDate { get; set; }

        public string FileType { get; set; }

        //will use to check whether or not that file has been uploaded before
        public byte[] Hash { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }

        public virtual ICollection<SoftwareDataset> SoftwareDatasets { get; set; }

    }
}

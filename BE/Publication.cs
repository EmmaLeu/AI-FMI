using System;
using System.Collections.Generic;
namespace BE
{
    public partial class Publication
    {   
        public Publication()
        {
            this.Images = new List<Image>();
        }

        public int PublicationID { get; set; }

        public int UserID { get; set; }

        public int? UploadID { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public int PublicationYear { get; set; }

        public string Pages { get; set; }

        public string Abstract { get; set; }

        public string KeyWords { get; set; }

        public string Journal { get; set; }

        public string Conference { get; set; }

        public string Book { get; set; }

        public string Volume { get; set; }

        public string Institution { get; set; }

        public string PatentOffice { get; set; }

        public string PatentNumber { get; set; }

        public string ApplicationNumber { get; set; }

        public string Issue { get; set; }

        public string Publisher { get; set; }

        public string Source { get; set; }

        public string Link { get; set; }

        public string LinkText { get; set; }

        public DateTime? CreationDate { get; set; }

        public virtual User User { get; set; }

        public virtual Upload Upload { get; set; }

        public virtual ICollection<Image> Images { get; set; }

    }
}

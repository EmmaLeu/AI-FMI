using System;

namespace BE
{
    public partial class Education
    {
        public int EducationID { get; set; }

        public int UserID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Position { get; set; }

        public string Institution { get; set; }

        public string Activities { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual User User { get; set; }
    }
}

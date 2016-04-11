using System;

namespace BE
{
    public partial class News
    {
        public int NewsID { get; set; }

        public int UserID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public string Link { get; set; }

        public string LinkText { get; set; }

        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public partial class Award
    {
        public int AwardID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public partial class SoftwareDataset
    {
        public SoftwareDataset()
        {
            Images = new List<Image>();
        }
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public string Authors { get; set; }

        public long CounterLinkViews { get; set; }

        public long CounterDownloads { get; set; }

        //Software and Datasets are kept together in DB. Type = 0 => Software, Type = 1 => Dataset
        public int? UploadID { get; set; }

        public bool Type { get; set; }

        public string Link { get; set; }

        public string LinkText { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual User User { get; set; }

        public virtual Upload Upload { get; set; }
    }
}

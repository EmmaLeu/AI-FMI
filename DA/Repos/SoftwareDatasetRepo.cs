using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Repos
{
    public class SoftwareDatasetRepo
    {
        private FMIContext context;

        public SoftwareDatasetRepo(FMIContext context)
        {
            this.context = context;
        }

        public List<SoftwareDataset> GetLatestItems(int howMany, bool type)
        {
            var items = context.Software
                .Where(i => i.Type == type)
                .Take(howMany)
                .ToList();

            return items != null? items : new List<SoftwareDataset>();
        }

        public List<SoftwareDataset> GetSoftwareDatasets(bool type)
        {
            var items = context.Software
                .Where(i => i.Type == type)
                .ToList();

            return items != null ? items : new List<SoftwareDataset>();
        }

        public void AddSoftwareDataset(SoftwareDataset sd)
        {
            var newSD = context.Software.Add(sd);

            if (sd.Upload != null)
            {
                var upload = context.Uploads.Add(sd.Upload);
                newSD.Upload = upload;
            }

            if (sd.Images != null && sd.Images.Count > 0)
            {
                foreach (var image in sd.Images)
                {
                    var newImg = context.Images.Add(image);
                    newImg.SoftwareDataset = newSD;
                }
            }

            context.SaveChanges();
        }
    }
}

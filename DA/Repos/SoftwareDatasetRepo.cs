using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
                .OrderByDescending(i => i.ID)
                .ToList();

            return items != null? items : new List<SoftwareDataset>();
        }

        public List<SoftwareDataset> GetSoftwareDatasets(bool type)
        {
            var items = context.Software
                .Where(i => i.Type == type)
                .OrderByDescending(i => i.ID)
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

        public SoftwareDataset DeleteSoftwareDataset(int sdId)
        {
            var deletedSD = new SoftwareDataset();

            try
            {
                var sd = context.Software
                    .Include(i => i.Upload)
                    .Include(i => i.Images)
                    .Where(i => i.ID == sdId)
                    .FirstOrDefault();

                if (sd != null)
                {
                    if (sd.Upload != null)
                    {
                        context.Uploads.Remove(sd.Upload);
                    }

                    if (sd.Images != null)
                    {
                        context.Images.RemoveRange(sd.Images);
                    }

                    deletedSD = context.Software.Remove(sd);
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return deletedSD;
        }
    }
}

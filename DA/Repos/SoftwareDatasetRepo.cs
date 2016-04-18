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
            var items = new List<SoftwareDataset>();
            try
            {
                items = context.Software
                .Where(i => i.Type == type)
                .OrderByDescending(i => i.ID)
                .ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return items;
        }

        public void AddSoftwareDataset(SoftwareDataset sd)
        {
            try
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
            }
            catch(Exception ex)
            {
                throw ex;
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

        public SoftwareDataset GetSoftwareDatasetById(int sdId)
        {
            var sd = new SoftwareDataset();
            try
            {
                sd = context.Software
                    .Include(i => i.Images)
                    .Include(i => i.Upload)
                    .Where(i => i.ID == sdId)
                    .FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return sd;
        }

        public void UpdateSoftwareDataset(SoftwareDataset sd, bool deleteImage, bool deleteUpload)
        {
            var sdToUpdate = context.Software
                .Include(i => i.Images)
                .Include(i => i.Upload)
                .Where(i => i.ID == sd.ID)
                .FirstOrDefault();

            var images = context.Images
                .Where(i => i.SoftwareDatasetID == sdToUpdate.ID)
                .ToList();

            if (sdToUpdate != null)
            {
                sdToUpdate.Title = sd.Title;
                sdToUpdate.Authors = sd.Authors;
                sdToUpdate.Type = sd.Type;
                sdToUpdate.Description = sd.Description;
                sdToUpdate.Link = sd.Link;
                sdToUpdate.LinkText = sd.LinkText;

                if (sdToUpdate.Upload != null && deleteUpload == true)
                {
                    context.Uploads.Remove(sdToUpdate.Upload);
                }

                if (sd.Upload != null)
                {
                    var upload = context.Uploads.Add(sd.Upload);
                    sdToUpdate.Upload = upload;
                }

                if (images != null && (deleteImage == true || (sd.Images != null && sd.Images.Count > 0)))
                {
                    foreach (var image in images)
                    {
                        context.Images.Remove(image);
                    }
                }

                if (sd.Images != null && sd.Images.Count > 0)
                {
                    foreach (var pubImage in sd.Images)
                    {
                        var newImage = context.Images.Add(pubImage);
                        newImage.SoftwareDataset = sdToUpdate;
                    }

                }
            }

            context.SaveChanges();
        }
    }
}


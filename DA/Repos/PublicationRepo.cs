using BE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace DA.Repos
{
    public class PublicationRepo
    {
        private FMIContext context;

        public PublicationRepo(FMIContext context)
        {
            this.context = context;
        }

        public void AddPublication(Publication publication)
        {
            
            var newPub = context.Publications.Add(publication);

            if (publication.Upload != null)
            {
                var upload = context.Uploads.Add(publication.Upload);
                newPub.Upload = upload;
            }

            if (publication.Images != null && publication.Images.Count > 0)
            {
                foreach (var image in publication.Images)
                {
                    var newImg = context.Images.Add(image);
                    newImg.Publication = newPub;
                }
            }
            
            context.SaveChanges();
        }

        public List<Publication> GetPublications()
        {
            return context.Publications
                .OrderByDescending(i => i.PublicationID)
                .Include(u => u.Upload)
                .Include(u => u.Images)
                .OrderByDescending(u => u.PublicationYear)
                .ThenByDescending(u => u.PublicationID)
                .ToList();
        }

        public List<Publication> GetLatestPublications(int howMany)
        {
            return context
                .Publications
                .OrderByDescending(i => i.PublicationYear)
                .ThenByDescending(i => i.CreationDate)
                .Take(howMany)
                .ToList();
        }

        public List<Publication> SearchPublications(string searchText)
        {
            return context
                .Publications
                .Where(i => i.KeyWords.Contains(searchText))
                .OrderByDescending(i => i.PublicationYear)
                .ThenByDescending(i => i.CreationDate)
                .ToList();
        }

        public Publication DeletePublication(int publicationId)
        {
            var deletedPublication = new Publication();

            try
            {
                var publication = context.Publications
                    .Include(i => i.Upload)
                    .Include(i => i.Images)
                    .Where(i => i.PublicationID == publicationId)
                    .FirstOrDefault();

                var images = context.Images.Where(i => i.PublicationID == publication.PublicationID).ToList();
                if (publication != null)
                {
                    if (publication.Upload != null)
                    {
                        context.Uploads.Remove(publication.Upload);
                    }

                    if (images != null)
                    {
                        context.Images.RemoveRange(images);
                    }

                    deletedPublication = context.Publications.Remove(publication);
                }

                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return deletedPublication;
        }

        public Publication GetPublicationById(int publicationId)
        {
            var publication = new Publication();

            try
            {
                publication = context.Publications
                    .Include(i => i.Upload)
                    .Include(i => i.Images)
                    .Where(i => i.PublicationID == publicationId)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return publication;
        }

        public void UpdatePublication(Publication publication, bool deleteImage, bool deleteUpload)
        {
                var pubToUpdate = context.Publications
                    .Include(i => i.Images)
                    .Include(i => i.Upload)
                    .Where(i => i.PublicationID == publication.PublicationID)
                    .FirstOrDefault();

                var images = context.Images
                    .Where(i => i.PublicationID == pubToUpdate.PublicationID)
                    .ToList();

                if(pubToUpdate != null)
                {
                    pubToUpdate.Title = publication.Title;
                    pubToUpdate.Authors = publication.Authors;
                    pubToUpdate.PublicationYear = publication.PublicationYear;
                    pubToUpdate.Category = publication.Category;
                    pubToUpdate.Journal = publication.Journal;
                    pubToUpdate.Conference = publication.Conference;
                    pubToUpdate.Book = publication.Book;
                    pubToUpdate.Volume = publication.Volume;
                    pubToUpdate.Institution = publication.Institution;
                    pubToUpdate.PatentOffice = publication.PatentOffice;
                    pubToUpdate.PatentNumber = publication.PatentNumber;
                    pubToUpdate.ApplicationNumber = publication.ApplicationNumber;
                    pubToUpdate.Issue = publication.Issue;
                    pubToUpdate.Pages = publication.Pages;
                    pubToUpdate.Publisher = publication.Publisher;
                    pubToUpdate.KeyWords = publication.KeyWords;
                    pubToUpdate.Abstract = publication.Abstract;
                    pubToUpdate.Source = publication.Source;
                    pubToUpdate.Link = publication.Link;
                    pubToUpdate.LinkText = publication.LinkText;

                if (pubToUpdate.Upload != null && deleteUpload == true)
                {
                    context.Uploads.Remove(pubToUpdate.Upload);
                }

                if (publication.Upload != null)
                {
                    var upload = context.Uploads.Add(publication.Upload);
                    pubToUpdate.Upload = upload;
                }

                if (images != null && (deleteImage == true || (publication.Images!=null && publication.Images.Count > 0)))
                {
                    foreach (var image in images)
                    {
                        context.Images.Remove(image);
                    }
                }

                if (publication.Images != null && publication.Images.Count > 0)
                {
                    foreach (var pubImage in publication.Images)
                    {
                        var newImage = context.Images.Add(pubImage);
                        newImage.Publication = pubToUpdate;
                    }

                }
            }

            context.SaveChanges();
        }
    }
}

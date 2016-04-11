using BE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
    }
}

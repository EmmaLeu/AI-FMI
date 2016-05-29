using BE;
using DA;
using System.Collections.Generic;

namespace BL
{
    public class PublicationService
    {
        private Repositories repository;

        public PublicationService(Repositories repository)
        {
            this.repository = repository;
        }

        public void AddPublication(Publication publication)
        {
            repository.PublicationRepo.AddPublication(publication);
        }

        public List<Publication> GetPublications(string sort)
        {
            return repository.PublicationRepo.GetPublications(sort);
        }
        
        public List<Publication> GetPublicationsPaged(string sort, int page, int itemsPerPage)
        {
            return repository.PublicationRepo.GetPublicationsPaged(sort, page, itemsPerPage);
        }
        public int GetPublicationCount(string category)
        {
            return repository.PublicationRepo.GetPublicationCount(category);
        }

        public List<Publication> GetLatestPublications(int howMany)
        {
            return repository.PublicationRepo.GetLatestPublications(howMany);
        }

        public List<Publication> SearchPublications(string searchText)
        {
            return repository.PublicationRepo.SearchPublications(searchText);
        }

        public Publication DeletePublication(int publicationId)
        {
            return repository.PublicationRepo.DeletePublication(publicationId);
        }

        public Publication GetPublicationById(int publicationId)
        {
            return repository.PublicationRepo.GetPublicationById(publicationId);
        }

        public void UpdatePublication(Publication publication, bool deleteImage, bool deleteUpload)
        {
            repository.PublicationRepo.UpdatePublication(publication, deleteImage, deleteUpload);
        }

        public List<string> GetCategories()
        {
            return repository.PublicationRepo.GetCategories();
        }
    }
}

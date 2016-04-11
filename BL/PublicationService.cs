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

        public List<Publication> GetPublications()
        {
            return repository.PublicationRepo.GetPublications();
        }

        public List<Publication> GetLatestPublications(int howMany)
        {
            return repository.PublicationRepo.GetLatestPublications(howMany);
        }

        public List<Publication> SearchPublications(string searchText)
        {
            return repository.PublicationRepo.SearchPublications(searchText);
        }
    }
}

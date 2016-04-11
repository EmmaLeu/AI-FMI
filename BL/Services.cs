using DA;
using System;


namespace BL
{
    public class Services : IDisposable
    {
        private Repositories repository;

        public Services()
        {
            repository = new Repositories();
        }

        private UserService userService;
        public UserService UserService
        {
            get
            {
                return userService ?? (userService = new UserService(repository));
            }
        }

        private EducationService educationService;
        public EducationService EducationService
        {
            get
            {
                return educationService ?? (educationService = new EducationService(repository));
            }
        }

        private PublicationService publicationService;
        public PublicationService PublicationService
        {
            get
            {
                return publicationService ?? (publicationService = new PublicationService(repository));
            }
        }
        private AwardService awardService;
        public AwardService AwardService
        {
            get
            {
                return awardService ?? (awardService = new AwardService(repository));
            }
        }

        private NewsService newsService;
        public NewsService NewsService
        {
            get
            {
                return newsService ?? (newsService = new NewsService(repository));
            }
        }

        private SoftwareDatasetService sdService;
        public SoftwareDatasetService SoftwareDatasetService
        {
            get
            {
                return sdService ?? (sdService = new SoftwareDatasetService(repository));
            }
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}

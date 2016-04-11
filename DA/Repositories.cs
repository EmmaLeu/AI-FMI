using DA.Repos;
using System;

namespace DA
{
    public class Repositories : IDisposable
    {
        private FMIContext context;

        public Repositories()
        {
            this.context = new FMIContext();
        }

        private UserRepo userRepo;
        public UserRepo UserRepo
        {
            get
            {
                return userRepo ?? (userRepo = new UserRepo(context));
            }
        }

        private EducationRepo educationRepo;
        public EducationRepo EducationRepo
        {
            get
            {
                return educationRepo ?? (educationRepo = new EducationRepo(context));
            }
        }

        private NewsRepo newsRepo;
        public NewsRepo NewsRepo
        {
            get
            {
                return newsRepo ?? (newsRepo = new NewsRepo(context));
            }
        }

        private AwardRepo awardRepo;
        public AwardRepo AwardRepo
        {
            get
            {
                return awardRepo ?? (awardRepo = new AwardRepo(context));
            }
        }

        private PublicationRepo publicationRepo;
        public PublicationRepo PublicationRepo
        {
            get
            {
                return publicationRepo ?? (publicationRepo = new PublicationRepo(context));
            }
        }

        private SoftwareDatasetRepo sdRepo;
        public SoftwareDatasetRepo SoftwareDatasetRepo
        {
            get
            {
                return sdRepo ?? (sdRepo = new SoftwareDatasetRepo(context));
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
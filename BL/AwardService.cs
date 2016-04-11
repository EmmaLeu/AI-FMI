using BE;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AwardService
    {
        private Repositories repository;

        public AwardService(Repositories repository)
        {
            this.repository = repository;
        }

        public List<Award> GetAwards()
        {
            return repository.AwardRepo.GetAwards();
        }

        public List<Award> GetUserAwards(int userID)
        {
            return repository.AwardRepo.GetUserAwards(userID);
        }

        public void UpdateAward(Award award)
        {
            repository.AwardRepo.UpdateAward(award);
        }
        public void DeleteAward(int awardID)
        {
            repository.AwardRepo.DeleteAward(awardID);
        }
        public void AddAward(Award award, User user)
        {
            repository.AwardRepo.AddAward(award, user);
        }
        public List<Award> GetLatestAwards(int howMany)
        {
            return repository.AwardRepo.GetLatestAwards(howMany);
        }
    }
}

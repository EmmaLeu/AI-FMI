using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Repos
{
    public class AwardRepo
    {
        private FMIContext context;

        public AwardRepo(FMIContext context)
        {
            this.context = context;
        }
        public List<Award> GetAwards()
        {
            return context.Awards
                .OrderByDescending(i => i.AwardID)
                .ToList();
        }

        public List<Award> GetUserAwards(int userID)
        {
            return context.Awards
                .Where(u => u.UserID == userID)
                .ToList();
        }

        public void AddAward(Award award, User user)
        {
            award.User = user;
            var newAward = context.Awards.Add(award);
            context.SaveChanges();
        }

        public void UpdateAward(Award award)
        {
            var awardToUpdate = context.Awards
                .Where(u => u.AwardID == award.AwardID)
                .FirstOrDefault();

            if (awardToUpdate != null)
            {
                awardToUpdate.Description = award.Description;
                awardToUpdate.Title = award.Title;
                context.SaveChanges();
            }
        }

        public void DeleteAward(int awardID)
        {
            var award = context.Awards.Where(u => u.AwardID == awardID).FirstOrDefault();
            context.Awards.Remove(award);
            context.SaveChanges();
        }
        public List<Award> GetLatestAwards(int howMany)
        {
            var list = context.Awards
                .OrderByDescending(i => i.AwardID)
                .Take(howMany)
                .ToList();

            return list;
        }
    }
}

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA;

namespace BL
{
    public static class DbIntializr
    {
        public static void InitializeDatabase()
        {
            Database.SetInitializer<FMIContext>(new CreateDatabaseIfNotExists<FMIContext>());
            //Database.SetInitializer<SocializrContext>(new MigrateDatabaseToLatestVersion<SocializrContext, Configuration>());
        }
    }
}

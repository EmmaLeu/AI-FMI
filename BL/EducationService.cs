using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EducationService
    {
        private Repositories repository;

        public EducationService(Repositories repository)
        {
            this.repository = repository;
        }
    }
}

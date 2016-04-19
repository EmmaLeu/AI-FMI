using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI.Models
{
    public class DashboardVM
    {
        public List<MemberVM> Members { get; set; }

        public List<CollaboratorVM> Collaborators { get; set; }

        public List<MemberVM> Formers { get; set; }

    }
}
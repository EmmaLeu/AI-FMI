using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum Ranks
    {
        [Display(Name = "Professor")]
        Professor =0,
        [Display(Name = "Associate Professor")]
        AssociateProfessor,
        [Display(Name = "Lecturer")]
        Lecturer,
        [Display(Name = "Teaching Assistant")]
        TeachingAssistant,
        [Display(Name = "Postdoc")]
        Postdoc,
        [Display(Name ="PhD Student")]
        PhdStudent
    }
}

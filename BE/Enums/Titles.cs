using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum Titles
    {
        [Display(Name = "Ms.")]
        Ms=0,
        [Display(Name = "Mrs.")]
        Mrs,
        [Display(Name = "Mr.")]
        Mr,
        [Display(Name = "Dr.")]
        Dr
    }
}

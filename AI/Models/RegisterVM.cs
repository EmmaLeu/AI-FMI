using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AI.Models
{
    public class RegisterVM
    {
        [Remote("IsEmailUnique", "Admin", ErrorMessage = "Email already in use.")]
        [Required(ErrorMessage = "Login Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string LoginEmail { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        public bool Gender { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public int RoleID { get; set; }

        public List<SelectListItem> Roles { get; set; }
    }
}
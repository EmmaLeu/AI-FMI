using AI.Code.Base;
using AI.Models;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using System.Web.Security;
using AI.Code.Salt;

namespace AI.Controllers
{
    [AllowAnonymous]
    public class AdminController : BaseController
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Register()
        {
            var model = new RegisterVM()
            {
                Roles = GetRoles()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterVM user)
        {
            if (ModelState.IsValid)
            {
                var pwd = new PasswordManager();
                //var password = Membership.GeneratePassword(10, 0);
                var password = "123456";
                string salt = null;
                var passwordHash = pwd.GeneratePasswordHash(password, out salt);

                var newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    LoginEmail = user.LoginEmail,
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    CreationDate = DateTime.Now,
                    Gender = user.Gender,
                    IsDeleted = false

                };
      
                Services.UserService.AddUser(newUser, user.RoleID);
                user.Roles = GetRoles();
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        public ActionResult Dashboard()
        {
            var users = Services.UserService.GetMembers();
            var collaborators = Services.UserService.GetCollaborators();
            /*var software = Services.UserService.GetSoftware();
            var awards = Services.UserService.GetAwards();
            var datasets = Services.UserService.GetDatasets();*/
            return View();
        }

        public JsonResult IsEmailUnique(string LoginEmail)
        {
            return Json(Services.UserService.IsEmailUnique(LoginEmail), JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> GetRoles()
        {
            return Services.UserService.GetRoles()
                .Select(c => new SelectListItem
                {
                    Value = c.RoleID.ToString(),
                    Text = c.Title
                }).ToList();
        }
    }
}
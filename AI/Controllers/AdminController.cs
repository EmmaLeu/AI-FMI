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
using AI.Code.Utils;

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

        [Authorize(Roles="Admin")]
        public ActionResult Dashboard()
        {
            var model = new DashboardVM()
            {
                Members = Services.UserService.GetMembers()
                .Select(i => new MemberVM()
                {
                    UserID = i.UserID,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Title = EnumHelp.GetDescription(i.Title),
                    Rank = EnumHelp.GetDescription(i.Rank),
                    ImageID = i.ImageID
                })
                .ToList(),

                Formers = Services.UserService.GetFormerMembers()
                .Select(i => new MemberVM()
                {
                    UserID = i.UserID,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Title = EnumHelp.GetDescription(i.Title),
                    ImageID = i.ImageID
                }).ToList(),

                Collaborators = Services.UserService.GetCollaborators()
                .Select(i => new CollaboratorVM()
                {
                    CollaboratorID = i.CollaboratorID,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Title = i.Title,
                    ImageID = i.ImageID,
                    Institution = i.Institution
                }).ToList()

            };

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateToFormerMember(int id)
        {
                if (Session.CurrentUser != null && Session.CurrentUser.IsAdmin)
                {
                    Services.UserService.UpdateToFormerMember(id);
                    return Json("ok");
                }

            return Json("nok");
        }

        [HttpGet]
        public ActionResult AddCollaborator()
        {
            var collaborator = new CollaboratorVM();
            return View(collaborator);
        }

        [HttpPost]
        public ActionResult AddCollaborator(CollaboratorVM col)
        {
            if(ModelState.IsValid)
            {
                var collaborator = new Collaborator()
                {
                    CreationDate = System.DateTime.Now,
                    FirstName = col.FirstName,
                    LastName = col.LastName,
                    Institution = col.Institution,
                };
                if (col.Title != null)
                    collaborator.Title = col.Title.ToString();
                Services.UserService.AddCollaborator(collaborator);
            }
            return RedirectToAction("Dashboard");
        }

        public ActionResult DeleteCollaborator(int id)
        {
            if (Session.CurrentUser != null)
            {
                Services.UserService.DeleteCollaborator(id);
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Login", "Account");
        }

        public JsonResult IsEmailUnique(string LoginEmail)
        {
            return Json(Services.UserService.IsEmailUnique(LoginEmail), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser(int id = 0)
        {
            if(Session.CurrentUser != null & Session.CurrentUser.IsAdmin)
            {
                if (id != 0)
                {
                    Services.UserService.DeleteUser(id);
                }
                return RedirectToAction("Dashboard");
            }

            return RedirectToAction("Login", "Account");
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
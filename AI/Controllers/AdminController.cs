﻿using AI.Code.Base;
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
using System.Net.Mail;
using System.Net;

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
                var link = "?password=" + password;
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(user.LoginEmail));  // replace with valid value 
                message.From = new MailAddress("radu.ionescu@gmail.com");  // replace with valid value
                message.Subject = "Account confirmation";
                message.Body = string.Format(body, message.From, "AI Research", "Please confirm by clicking this link ");
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "user@outlook.com",  // replace with valid value
                        Password = "password"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                   // await smtp.SendMailAsync(message);
                    //return RedirectToAction("Sent");
                }

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
        public JsonResult UpdateToFormerOrMember(int id, bool isDeleted)
        {
                if (Session.CurrentUser != null && Session.CurrentUser.IsAdmin)
                {
                    Services.UserService.UpdateToFormerMember(id, isDeleted);
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

        [HttpPost]
        public JsonResult DeleteCollaborator(int id)
        {
            if (Session.CurrentUser != null)
            {
                Services.UserService.DeleteCollaborator(id);
                return Json("ok");
            }
            return Json("nok");
        }

        public JsonResult IsEmailUnique(string LoginEmail)
        {
            return Json(Services.UserService.IsEmailUnique(LoginEmail), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUser(int id = 0)
        {
            if(Session.CurrentUser != null & Session.CurrentUser.IsAdmin)
            {
                if (id != 0)
                {
                    Services.UserService.DeleteUser(id);
                }
                return Json("ok");
            }

            return Json("nok");
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
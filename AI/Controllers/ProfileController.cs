using AI.Code.Base;
using AI.Models;
using BE;
using BE.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AI.Code.Utils;

namespace AI.Controllers
{
    [AllowAnonymous]
    public class ProfileController : BaseController
    {
        // GET: Profile/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id=null)
        {
            if (Session.CurrentUser != null)
            {
                var userID = Session.CurrentUser.UserID;

                var user = Services.UserService.GetUserWithEducation(userID);
                if (ModelState.IsValid)
                {
                    var userInfo = new UserInfoVM()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Birthday = user.Birthday,
                        Gender = user.Gender,
                        Nationality = user.Nationality,
                        InterestAreas = user.InterestAreas,
                        ContactEmail = user.ContactEmail,
                        CurrentInsitution = user.CurrentInsitution,
                        EducationList = user.EducationList.Select(i => new EducationVM()
                        {
                            EducationID = i.EducationID,
                            StartDate = i.StartDate,
                            EndDate = i.EndDate,
                            UserID = i.UserID,
                            Institution = i.Institution,
                            Activities = i.Activities

                        }).ToList()
                    };

                    return View(userInfo);
                }
            }
            return RedirectToAction("Login", "Account");
        }
       
        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(UserInfoVM user)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = new User()
                {
                    UserID = Session.CurrentUser.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Birthday = user.Birthday,
                    Gender = user.Gender,
                    Nationality = user.Nationality,
                    InterestAreas = user.InterestAreas,
                    ContactEmail = user.ContactEmail,
                    CurrentInsitution = user.CurrentInsitution
                };
                if (user.Title != null)
                    userToUpdate.Title = user.Title.ToString();
                if (user.Rank != null)
                    userToUpdate.Rank = user.Rank.ToString();

                Services.UserService.UpdateUserInformation(userToUpdate);
                Session.CurrentUser.FirstName = userToUpdate.FirstName;
                Session.CurrentUser.LastName = userToUpdate.LastName;
                Session.CurrentUser.Rank = EnumHelp.GetDescription(userToUpdate.Rank);
                return RedirectToAction("Edit");
            }
      
               return View(user);
        }

        [HttpGet]
        public ActionResult ViewProfile(int id)
        {
            var user = Services.UserService.GetUserWithEducation(id);
            var userInfo = new UserInfoVM()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Gender = user.Gender,
                Nationality = user.Nationality,
                InterestAreas = user.InterestAreas,
                ContactEmail = user.ContactEmail,
                CurrentInsitution = user.CurrentInsitution,
                EducationList = user.EducationList.Select(i => new EducationVM()
                {
                    EducationID = i.EducationID,
                    StartDate = i.StartDate,
                    EndDate = i.EndDate,
                    UserID = i.UserID,
                    Institution = i.Institution,
                    Activities = i.Activities

                }).ToList()
            };
                return View(userInfo);

            }

        [HttpGet]
        public ActionResult AddJournal()
        {
            var journalPublication = new PublicationVM();
            return View(journalPublication);
        }

        [HttpPost]
        public ActionResult AddPublication(PublicationVM publication, string category)
        {
            if (Session.CurrentUser != null)
            {
                var userID = Session.CurrentUser.UserID;
                if (ModelState.IsValid)
                {
                    var newPub = GetPublicationInformation(publication, category, userID);
                    Services.PublicationService.AddPublication(newPub);
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
                return View("Add" + category, publication);

            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult AddSoftware()
        {
            var softwareDataset = new SoftwareDatasetVM();
            return View(softwareDataset);
        }

        [HttpGet]
        public ActionResult AddDataset()
        {
            var softwareDataset = new SoftwareDatasetVM();
            return View(softwareDataset);
        }

        //this action adds both Software and Datasets
        [HttpPost]
        public ActionResult AddSoftware(SoftwareDatasetVM sd)
        {
            if (Session.CurrentUser != null)
            {
                var userID = Session.CurrentUser.UserID;
                if (ModelState.IsValid)
                {
                    var newPub = GetSoftwareInformation(sd, userID);
                    Services.SoftwareDatasetService.AddSoftwareDataset(newPub);
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
                return View("Add" + (sd.Type == false ? "Software" : "Dataset"), sd);

            }
            return RedirectToAction("Login", "Account");
        }

        

        public ActionResult GetProfilePicture(int? id = null)
        {
            //if(!id.HasValue)
                return GetDefaultProfileImage();
            
            //continue here with profile picture by id for each user
        }

       /* [HttpGet]
        public ActionResult UploadProfilePicture(HttpPostedFileBase file)
        {

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                var guid = Guid.NewGuid().ToString();
                // store the file inside ~/App_Data/Images folder
                var path = Path.Combine(Server.MapPath("~/App_Data/Images"), guid);
                file.SaveAs(path);
                var userID = Session.CurrentUser.UserID;
                var profilePic = new Image()
                {
                    UserID = userID,
                    Name = guid,
                    FileSize = file.ContentLength,
                    FilePath = path,
                    UploadDate = System.DateTime.Now
                };

                var isOkPath = Services.UserService.AddProfilePicture(profilePic, userID);
                if(isOkPath!=null)
                { 
                    var hasValidPath = System.IO.File.Exists(path);
                    if (!hasValidPath)
                        throw new ApplicationException("Profile picture does not exist!");

                    return File(System.IO.File.ReadAllBytes(path),file.ContentType);
                }
            }
            // redirect back to the index action to show the form once again
            return View();

        }*/

        [HttpGet]
        public ActionResult AddConference()
        {
            var conferencePub = new PublicationVM();
            return View(conferencePub);
        }
           
        [HttpGet]
        public ActionResult AddChapter()
        {
            var chapterPublication = new PublicationVM();
            return View(chapterPublication);
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            var bookPublication = new PublicationVM();
            return View(bookPublication);
        }

        [HttpGet]
        public ActionResult AddThesis()
        {
            var thesisPublication = new PublicationVM();
            return View(thesisPublication);
        }

        [HttpGet]
        public ActionResult AddPatent()
        {
            var patentPublication = new PublicationVM();
            return View(patentPublication);
        }

        [HttpGet]
        public ActionResult AddOther()
        {
            var otherPublication = new PublicationVM();
            return View(otherPublication);
        }

        private Publication GetPublicationInformation (PublicationVM publication, string category, int userID)
        {

            var publicationToAdd = new Publication()
            {
                UserID = userID,
                Title = publication.Title,
                Authors = publication.Authors,
                PublicationYear = Convert.ToInt32(publication.PublicationDate),
                CreationDate = DateTime.Now,
                Category = category,
                Journal = publication.Journal,
                Conference = publication.Conference,
                Book = publication.Book,
                Volume = publication.Volume,
                Institution = publication.Institution,
                PatentOffice = publication.PatentOffice,
                PatentNumber = publication.PatentNumber,
                ApplicationNumber = publication.ApplicationNumber,
                Issue = publication.Issue,
                Pages = publication.Pages,
                Publisher = publication.Publisher,
                KeyWords = publication.KeyWords,
                Abstract = publication.Abstract,
                Source = publication.Source,
                Link = publication.Link,
                LinkText = publication.LinkText
            };

            var image = publication.Image;
            if (image != null && image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/images"), guid + '-' + fileName);
                image.SaveAs(path);

                var newImage = new BE.Image()
                {
                    UserID = userID,
                    Name = guid + "-" + fileName,
                    FileSize = image.ContentLength,
                    FilePath = path,
                    CreationDate = System.DateTime.Now,
                    ImageType = image.ContentType
                };

                publicationToAdd.Images.Add(newImage);
            }

            var upload = publication.Upload;
            if (upload != null && upload.ContentLength > 0)
            {

                var fileName = Path.GetFileName(upload.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/uploads"), guid + "-" + fileName);
                upload.SaveAs(path);

                var newUpload = new BE.Upload()
                {
                    FileName = guid + "-" + fileName,
                    FileSize = upload.ContentLength,
                    FilePath = path,
                    CreationDate = System.DateTime.Now,
                    FileType = upload.ContentType
                };

                publicationToAdd.Upload = newUpload;
            }

            return publicationToAdd;
        }

        private SoftwareDataset GetSoftwareInformation(SoftwareDatasetVM sd, int userID)
        {
            var sdToAdd = new SoftwareDataset()
            {
                UserID = userID,
                Title = sd.Title,
                Authors = sd.Authors,
                CreationDate = DateTime.Now,
                Type = sd.Type,
                Link = sd.Link,
                LinkText = sd.LinkText,
                Description = sd.Description
            };

            var image = sd.Image;
            if (image != null && image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/images"), guid + '-' + fileName);
                image.SaveAs(path);

                var newImage = new BE.Image()
                {
                    UserID = userID,
                    Name = guid + "-" + fileName,
                    FileSize = image.ContentLength,
                    FilePath = path,
                    CreationDate = System.DateTime.Now,
                    ImageType = image.ContentType
                };

                sdToAdd.Images.Add(newImage);
            }

            var upload = sd.Upload;
            if (upload != null && upload.ContentLength > 0)
            {

                var fileName = Path.GetFileName(upload.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/uploads"), guid + "-" + fileName);
                upload.SaveAs(path);

                var newUpload = new BE.Upload()
                {
                    FileName = guid + "-" + fileName,
                    FileSize = upload.ContentLength,
                    FilePath = path,
                    CreationDate = System.DateTime.Now,
                    FileType = upload.ContentType
                };

                sdToAdd.Upload = newUpload;
            }

            return sdToAdd;
        }

        public ActionResult GetDefaultProfileImage()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\images\default.png");
            var hasValidPath = System.IO.File.Exists(path);
            if (!hasValidPath)
                throw new ApplicationException("Default image does not exist!");

            return File(System.IO.File.ReadAllBytes(path),".png");
        }

        public string GetDescription(string title)
        {
            if (title == "Ms")
                return "Ms.";
            if (title == "Mr")
                return "Mr.";
            if (title == "Mrs")
                return "Mrs.";
            if (title == "Dr")
                return "Dr.";
            return string.Empty;
        }
    }
}

using AI.Code.Base;
using AI.Code.Utils;
using AI.Models;
using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace AI.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult ViewMembers()
        {
            var members = new MembersVM()
            {
                Members = Services.UserService.GetMembers()
                .Where(i => i.IsDeleted == false)
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

                FormerMembers = Services.UserService.GetFormerMembers()
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
                    Title = EnumHelp.GetDescription(i.Title),
                    ImageID = i.ImageID,
                    Institution = i.Institution
                }).ToList()

            };
           
            return View(members);
        
    }

        public ActionResult Index()
        {
            var recentUpdates = new RecentUpdatesVM()
            {
                Publications = new List<PublicationVM>(),
                News = new List<NewsVM>(),
                Awards = new List<AwardVM>(),
                Software = new List<SoftwareDatasetVM>(),
                Datasets = new List<SoftwareDatasetVM>()
            };

            GetLatestPublications(recentUpdates);
            GetLatestNews(recentUpdates);
            GetLatestAwards(recentUpdates);
            GetLatestSoftware(recentUpdates);
            GetLatestDatasets(recentUpdates);
            var path = Path.Combine(Server.MapPath("~/XMLCover.xml"));
            if (System.IO.File.Exists(path))
            {
                XDocument xdoc = XDocument.Load(path);
                var element = xdoc.Elements("caption").Single().Value;
                recentUpdates.CoverCaption = element;
            }
            
            return View(recentUpdates);
        }

        [HttpGet]
        public ActionResult Awards()
        {
            var awards = new AwardsVM()
            {
                Awards = Services.AwardService.GetAwards().Select(i => new AwardVM()
                {
                    AwardID = i.AwardID,
                    Title = i.Title,
                    Description = i.Description,
                    UserID = i.UserID,
                    FullName = i.User.FirstName + " " + i.User.LastName

                }).ToList()
            };
            return View(awards);
        }

        [HttpPost]
        public ActionResult AddAward(AwardVM award)
        {
            if(ModelState.IsValid)
            {
                if (Session.CurrentUser != null)
                {
                    var userID = Session.CurrentUser.UserID;
                    var user = Services.UserService.GetUserById(userID);
                    var newAward = new Award()
                    {
                        Title = award.Title,
                        Description = award.Description,
                        CreationDate = System.DateTime.Now,
                        UserID = userID
                    };
                    Services.AwardService.AddAward(newAward, user);
                    return Json("ok");
                }
                ModelState.AddModelError("", "Title field is required.");
            }
            return Json("nok");
        }

        [HttpPost]
        public ActionResult DeleteAward(int awardID)
        {

                Services.AwardService.DeleteAward(awardID);
                return Json("ok");
        }

        [HttpPost]
        public ActionResult UpdateAward(AwardVM award)
        {
            if (ModelState.IsValid)
            {
                var updatedAward = new Award()
                {
                    AwardID = award.AwardID,
                    Description = award.Description,
                    Title = award.Title
                };

                Services.AwardService.UpdateAward(updatedAward);
                return Json("ok");
            }
            ModelState.AddModelError("", "One or more fields are invalid.");
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [HttpGet]
        public ActionResult Software()
        {
            var software = Services.SoftwareDatasetService
                .GetSoftwareDatasets(false).Select(
                i => new SoftwareDatasetVM()
                {
                    ID = i.ID,
                    UserID = i.UserID,
                    Authors = i.Authors,
                    Description = i.Description,
                    CounterDownloads = i.CounterDownloads,
                    CounterLinkViews = i.CounterLinkViews,
                    Link = i.Link,
                    LinkText = i.LinkText,
                    Title = i.Title,
                    CreationDate = i.CreationDate,
                    ImageName = i.Images.Select(u => u.Name).FirstOrDefault(),
                    UploadName = (i.Upload != null ? i.Upload.FileName : null)
                }).ToList();

            return View(software);
        }

        [HttpGet]
        public ActionResult Datasets()
        {
            var dataset = Services.SoftwareDatasetService
                .GetSoftwareDatasets(true).Select(
                i => new SoftwareDatasetVM()
                {
                    ID = i.ID,
                    UserID = i.UserID,
                    Authors = i.Authors,
                    Description = i.Description,
                    CounterDownloads = i.CounterDownloads,
                    CounterLinkViews = i.CounterLinkViews,
                    Link = i.Link,
                    LinkText = i.LinkText,
                    Title = i.Title,
                    CreationDate = i.CreationDate,
                    ImageName = i.Images.Select(u => u.Name).FirstOrDefault(),
                    UploadName = (i.Upload != null ? i.Upload.FileName : null)
                }).ToList();

            return View(dataset);
        }

        [HttpPost]
        public JsonResult UpdateLinkViews(int id = 0)
        {
            if(id != 0)
            {
                var userId = Services.SoftwareDatasetService.GetSoftwareDatasetById(id).UserID;
                if(Session.CurrentUser !=null && Session.CurrentUser.UserID == userId)
                {
                    return Json("ok");
                }
                Services.SoftwareDatasetService.UpdateLinkViews(id);
                return Json("ok");
            }
            return Json("nok");
        }

        [HttpPost]
        public JsonResult UpdateDownloads(int id = 0)
        {
            if (id != 0)
            {
                var userId = Services.SoftwareDatasetService.GetSoftwareDatasetById(id).UserID;
                if (Session.CurrentUser != null && Session.CurrentUser.UserID == userId)
                {
                    return Json("ok");
                }
                Services.SoftwareDatasetService.UpdateDownloads(id);
                return Json("ok");
            }
            return Json("nok");
        }

        [HttpGet]
        public ActionResult News(int page = 1)
        {
            var model = new NewsPageVM()
            {
                NewsList = new List<NewsVM>(),
                NoOfPages = CalculatePages(Services.NewsService.GetNewsCount(), 5)
            };

            if(page >= 0 && page <= model.NoOfPages)
            {
                model.CurrentPage = page;
            }
            else
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }

            List<NewsVM> newsList = Services.NewsService.GetPagedNews(page, 5).Select(i => new NewsVM()
            {
                NewsID = i.NewsID,
                Title = i.Title,
                Description = i.Description,
                Link = i.Link,
                LinkText = i.LinkText,
                UserID = i.UserID

            }).ToList();

            model.NewsList = newsList;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddNews(NewsVM news)
        {
            if (ModelState.IsValid)
            {
                if (Session.CurrentUser != null)
                {
                    var userID = Session.CurrentUser.UserID;
                    var user = Services.UserService.GetUserById(userID);
                    var newNews = new News()
                    {
                        Title = news.Title,
                        Description = news.Description,
                        CreationDate = System.DateTime.Now,
                        Link = news.Link,
                        LinkText = news.LinkText,
                        UserID = userID
                    };
                    Services.NewsService.AddNews(newNews, user);
                    return Json("ok");
                }
                ModelState.AddModelError("", "Title field is required.");
            }
            return Json("nok");
        }

        [HttpPost]
        public ActionResult DeleteNews(int newsID)
        {

            Services.NewsService.DeleteNews(newsID);
            return Json("ok");
        }

        [HttpPost]
        public ActionResult UpdateNews(NewsVM news)
        {
            if (ModelState.IsValid)
            {
                var updatedNews = new News()
                {
                    NewsID = news.NewsID,
                    Description = news.Description,
                    CreationDate = news.CreationDate,
                    Title = news.Title,
                    Link = news.Link,
                    LinkText = news.LinkText,
                    UserID = news.UserID
                };

                Services.NewsService.UpdateNews(updatedNews);
                return Json("ok");
            }
            ModelState.AddModelError("", "One or more fields are invalid.");
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult Publications(string searchText, bool isSearch = false, string sort = "All", int page = 1)
        {
            var items = new PublicationsVM()
            {
                SearchText = searchText,
                Categories = PopulateCategories(),
                Category = sort
            };

            items.NoOfPages = CalculatePages(Services.PublicationService.GetPublicationCount(sort), 5);          

            if (!isSearch)
            {
                if (page >= 0 && page <= items.NoOfPages)
                {
                    items.CurrentPage = page;
                }
                else
                {
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
                items.Publications = Services.PublicationService.GetPublicationsPaged(sort, page, 5)
                .Select(i => new PublicationVM()
                {
                    PublicationID = i.PublicationID,
                    UserID = i.UserID,
                    Title = i.Title,
                    Authors = i.Authors,
                    PublicationDate = Convert.ToString(i.PublicationYear),
                    Category = i.Category,
                    Journal = i.Journal,
                    Conference = i.Conference,
                    Book = i.Book,
                    Volume = i.Volume,
                    Institution = i.Institution,
                    PatentOffice = i.PatentOffice,
                    PatentNumber = i.PatentNumber,
                    ApplicationNumber = i.ApplicationNumber,
                    Issue = i.Issue,
                    Pages = i.Pages,
                    Publisher = i.Publisher,
                    KeyWords = i.KeyWords,
                    KeyWordsList =(i.KeyWords != null) ? i.KeyWords.Split(',', ' ', '.', ';', '-').ToList() : new List<string>(),
                    Abstract = i.Abstract,
                    Link = i.Link,
                    LinkText = i.LinkText,
                    Source = i.Source,
                    CreationDate = i.CreationDate,
                    ImageName = i.Images.Select(u => u.Name).FirstOrDefault(),
                    UploadName = (i.Upload != null ? i.Upload.FileName : null)

                }).ToList();
            }
            else
            {
                items.CurrentPage = 1;
                items.Publications = Services.PublicationService.SearchPublications(searchText).Select(i => new PublicationVM()
                {
                    PublicationID = i.PublicationID,
                    UserID = i.UserID,
                    Title = i.Title,
                    Authors = i.Authors,
                    PublicationDate = Convert.ToString(i.PublicationYear),
                    Category = i.Category,
                    Journal = i.Journal,
                    Conference = i.Conference,
                    Book = i.Book,
                    Volume = i.Volume,
                    Institution = i.Institution,
                    PatentOffice = i.PatentOffice,
                    PatentNumber = i.PatentNumber,
                    ApplicationNumber = i.ApplicationNumber,
                    Issue = i.Issue,
                    Pages = i.Pages,
                    Publisher = i.Publisher,
                    KeyWordsList = (i.KeyWords != null) ? i.KeyWords.Split(',', ' ', '.', ';', '-').ToList() : new List<string>(),
                    Abstract = i.Abstract,
                    Link = i.Link,
                    Source = i.Source,
                    CreationDate = i.CreationDate,
                    ImageName = i.Images.Select(u => u.Name).FirstOrDefault(),
                    UploadName = (i.Upload != null ? i.Upload.FileName : null)

                }).ToList();
            }
            return View(items);
        }

        public FileResult Download(string fileName)
        {
            
            var filePath = Path.Combine(Server.MapPath("~/uploads"), fileName);
            if (System.IO.File.Exists(filePath))
            {
                var data = System.IO.File.ReadAllBytes(filePath);
                return File(data, MimeMapping.GetMimeMapping(fileName));
            }
            else
            {
                throw new Exception("The file does not exist!");
            }
        }

        public ActionResult UploadCoverImage(HttpPostedFileBase image)
        {
            if(image != null)
            {
                var fullPath = Request.MapPath("~/images/" + "cover.jpg");
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                var path = Path.Combine(Server.MapPath("~/images"), "cover.jpg");
                image.SaveAs(path);
                AddCaptionToCoverImage("");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddCaptionToCoverImage(string caption="")
        {
            var path = Path.Combine(Server.MapPath("~/XMLCover.xml"));
            if (System.IO.File.Exists(path))
            {
                XDocument xdoc = XDocument.Load(path);
                var element = xdoc.Elements("caption").Single();
                element.Value = caption;
                xdoc.Save(Path.Combine(Server.MapPath("~/XMLCover.xml")));
                return Json("ok");
            }

            return Json("nok"); 
        }

        public ActionResult GetImage(string fileName)
        {
            var fullPath = Path.Combine(Server.MapPath("~/images"), fileName);
            var hasValidPath = System.IO.File.Exists(fullPath);
            if (!hasValidPath)
                throw new ApplicationException("Default image does not exist!");

            return File(System.IO.File.ReadAllBytes(fullPath), ".png");
        }

        public ActionResult Contact()
         {
             ViewBag.Message = "Your contact page.";

             return View();
         }

        private void GetLatestAwards(RecentUpdatesVM recentUpdates)
        {
            recentUpdates.Awards = Services.AwardService.GetLatestAwards(5).Select(i => new AwardVM()
            {
                AwardID = i.AwardID,
                Description = i.Description,
                Title = i.Title,
                UserID = i.UserID,
                CreationDate = i.CreationDate,
                FullName = i.User.FirstName + " " + i.User.LastName
            }).ToList();
        }

        private void GetLatestNews(RecentUpdatesVM recentUpdates)
        {
            recentUpdates.News = Services.NewsService.GetLatestNews(5).Select(i => new NewsVM()
            {
                NewsID = i.NewsID,
                Description = i.Description,
                Link = i.Link,
                Title = i.Title,
                UserID = i.UserID,
                CreationDate = i.CreationDate
            }).ToList();
        }

        private void GetLatestPublications(RecentUpdatesVM recentUpdates)
        {
            recentUpdates.Publications = Services.PublicationService.GetLatestPublications(5).Select(i => new PublicationVM()
            {
                Authors = i.Authors,
                ApplicationNumber = i.ApplicationNumber,
                Book = i.Book,
                Category = i.Category,
                Conference = i.Conference,
                PublicationDate = Convert.ToString(i.PublicationYear),
                CreationDate = i.CreationDate,
                Link = i.Link,
                LinkText = i.LinkText,
                Institution = i.Institution,
                Issue = i.Issue,
                Journal = i.Journal,
                Pages = i.Pages,
                PatentNumber = i.PatentNumber,
                PatentOffice = i.PatentOffice,
                Publisher = i.Publisher,
                Source = i.Source,
                Title = i.Title,
                Volume = i.Volume

            }).ToList();
        }

        private void GetLatestSoftware(RecentUpdatesVM recentUpdates)
        {
            recentUpdates.Software = Services.SoftwareDatasetService.GetLatestItems(5, false).Select(
                i => new SoftwareDatasetVM()
                {
                    Authors = i.Authors,
                    Description = i.Description,
                    CounterDownloads = i.CounterDownloads,
                    CounterLinkViews = i.CounterLinkViews,
                    Link = i.Link,
                    LinkText = i.LinkText,
                    Title = i.Title,
                    CreationDate = i.CreationDate
                }).ToList();
        }

        private void GetLatestDatasets(RecentUpdatesVM recentUpdates)
        {
            recentUpdates.Datasets = Services.SoftwareDatasetService.GetLatestItems(5, true).Select(
                i => new SoftwareDatasetVM()
                {
                    Authors = i.Authors,
                    Description = i.Description,
                    CounterDownloads = i.CounterDownloads,
                    CounterLinkViews = i.CounterLinkViews,
                    Link = i.Link,
                    LinkText = i.LinkText,
                    Title = i.Title,
                    CreationDate = i.CreationDate
                }).ToList();
        }

        private List<CategoryVM> PopulateCategories()
        {
            var categories = new List<CategoryVM>();
            categories.Add(new CategoryVM() { Id = "All", Name = "All" });
            categories.Add(new CategoryVM() { Id = "Book", Name = "Book" });
            categories.Add(new CategoryVM() { Id = "Chapter", Name = "Chapter" });
            categories.Add(new CategoryVM() { Id = "Conference", Name = "Conference" });
            categories.Add(new CategoryVM() { Id = "Journal", Name = "Journal" });
            categories.Add(new CategoryVM() { Id = "Patent", Name = "Patent" });
            categories.Add(new CategoryVM() { Id = "Thesis", Name = "Thesis" });
            categories.Add(new CategoryVM() { Id = "Other", Name = "Other" });
            return categories;

        }

        private int CalculatePages(int total, int items)
        {
            return Convert.ToInt32(Math.Ceiling(total / (float)items));
        }
    }

}
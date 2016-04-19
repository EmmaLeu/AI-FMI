using BE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AI.Models
{
    public class MembersVM
    {
        public List<MemberVM> Members { get; set; }
        public List<MemberVM> FormerMembers { get; set; }
        public List<CollaboratorVM> Collaborators { get; set; }
    }

    public class MemberVM
    {
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? ImageID { get; set; }

        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public string Rank { get; set; }
    }

    public class CollaboratorVM
    {
        public int CollaboratorID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? ImageID { get; set; }

        public string Title { get; set; }

        public string Institution{ get; set; }
    }

    public class AwardVM
    {
        [Key]
        public int AwardID { get; set; }
        public int UserID { get; set; }
        [Required]
        public string Title { get; set; }

        [MaxLength(1000, ErrorMessage = "The message is too long. Please do not exceed 1000 characters.")]
        public string Description { get; set; }
        public string FullName { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class AwardsVM
    {
        public List<AwardVM> Awards { get; set; }
    }

    public class RecentUpdatesVM
    {
        public List<AwardVM> Awards { get; set; }
        public List<NewsVM> News { get; set; }
        public List<SoftwareDatasetVM> Software { get; set; }
        public List<SoftwareDatasetVM> Datasets { get; set; }
        public List<PublicationVM> Publications { get; set; }
    }

    public class NewsVM
    {
        [Key]
        public int NewsID { get; set; }

        public int UserID { get; set; }

        public DateTime CreationDate { get; set; }

        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Link { get; set; }

        public string LinkText { get; set; }
    }

    public class SoftwareDatasetVM
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "Authors field is required.")]
        public string Authors { get; set; }

        public long CounterLinkViews { get; set; }

        public long CounterDownloads { get; set; }

        public string Link { get; set; }

        public string LinkText { get; set; }

        //Software and Datasets are kept together in DB. Type = 0 => Software, Type = 1 => Dataset
        public bool Type { get; set; }

        public HttpPostedFileBase Upload { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string ImageName { get; set; }

        public string UploadName { get; set; }

        public bool DeleteImage { get; set; }
        public bool DeleteFile { get; set; }
    }
}

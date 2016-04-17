using BE;
using BE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AI.Models
{
    public class UserInfoVM
    {
        public UserInfoVM()
        {
            EducationList = new List<EducationVM>();
            Publications = new List<PublicationVM>();
            Software = new List<SoftwareDatasetVM>();
            Datasets = new List<SoftwareDatasetVM>();
        }

        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = ("The first name is requiered."))]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ("The last name is requiered."))]
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "MM/dd/yy")]
        public DateTime? Birthday { get; set; }

        public bool Gender { get; set; }

        public string Nationality { get; set; }

        public int? ImageID { get; set; }

        [Display(Name = "Title")]
        public Titles? Title { get; set; }

        [Display(Name = "Rank")]
        public Ranks? Rank { get; set; }

        public string InterestAreas { get; set; }

        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string ContactEmail { get; set; }

        public string CurrentInsitution { get; set; }

        public List<EducationVM> EducationList { get; set; }

        public List<PublicationVM> Publications { get; set; }

        public List<SoftwareDatasetVM> Software { get; set; }

        public List<SoftwareDatasetVM> Datasets { get; set; }
    }

    public class EducationVM
    {
        [Key]
        public int EducationID { get; set; }

        public int UserID { get; set; }

        [DisplayFormat(DataFormatString = "MM-yy")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "MM-yy")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public string Position { get; set; }

        public string Institution { get; set; }

        public string Activities { get; set; }
    }

    public class PublicationVM
    {
        public PublicationVM()
        {
            KeyWordsList = new List<string>();
        }

        public int PublicationID { get; set; }

        public int UserID { get; set; }

        public int? UploadID { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        [Required(ErrorMessage = "Authors are requiered.")]
        public string Authors { get; set; }

        [Display(Name = "Publication Year")]
        [Required(ErrorMessage = "Publication year is required.")]
        [RegularExpression("^\\d{4}$", ErrorMessage = "The input does not correspond to an year.")]
        public string PublicationDate { get; set; }

        public string Pages { get; set; }

        public string Abstract { get; set; }

        [Display(Name = "Key Words")]
        public string KeyWords { get; set; }

        public List<string> KeyWordsList { get; set; }

        public string Journal { get; set; }

        public string Conference { get; set; }

        public string Book { get; set; }

        public string Volume { get; set; }

        public string Institution { get; set; }

        [Display(Name = "Patent Office")]
        public string PatentOffice { get; set; }

        [Display(Name = "Patent Number")]
        public string PatentNumber { get; set; }

        [Display(Name = "Application Number")]
        public string ApplicationNumber { get; set; }

        public string Issue { get; set; }

        public string Publisher { get; set; }

        public string Source { get; set; }

        public string Link { get; set; }

        public string LinkText { get; set; }

        public DateTime? CreationDate { get; set; }

        public HttpPostedFileBase Upload { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string ImageName { get; set; }

        public string UploadName { get; set; }
    }

    public class PublicationsVM
    {
        public List<PublicationVM> Publications { get; set; }

        public string SearchText { get; set; }

    }

}

   /* public class JournalVM
    {
        [Key]
        public int PublicationID { get; set; }

        public int UserID { get; set; }
       
        public int? 
        { get; set; }

        public Category Category = Category.Journal;

        public string Title { get; set; }

        [Required]
        public string Authors { get; set; }

        [Display(Name = "Publication Date")]
        [Required]
        public string PublicationDate{ get; set; }

        public string  Journal { get; set; }

        public string Volume { get; set; }

        public string Issue { get; set; }

        public string Pages { get; set; }

        public string Publisher { get; set; }

        [Display(Name = "Key Words")]
        public string KeyWords { get; set; }

        public string Abstract { get; set; }

    }

    public class ConferenceVM
    {
        [Key]
        public int PublicationID { get; set; }
        [Key]
        public int UserID { get; set; }
        [Key]
        public int? UploadID { get; set; }

        public Category Category = Category.Conference;

        public string Title { get; set; }

        public string Authors { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        public string Conference { get; set; }

        public string Volume { get; set; }

        public string Issue { get; set; }

        public string Pages { get; set; }

        public string Publisher { get; set; }

        [Display(Name = "Key Words")]
        public string KeyWords { get; set; }

        public string Abstract { get; set; }
    }

    public class ChapterVM
    {
        [Key]
        public int PublicationID { get; set; }
        [Key]
        public int UserID { get; set; }
        [Key]
        public int? UploadID { get; set; }

        public Category Category = Category.Chapter;

        public string Title { get; set; }

        public string Authors { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        public string Book { get; set; }

        public string Volume { get; set; }

        public string Pages { get; set; }

        public string Publisher { get; set; }

        [Display(Name = "Key Words")]
        public string KeyWords { get; set; }

        public string Abstract { get; set; }
    }

    public class BookVM
    {
        [Key]
        public int PublicationID { get; set; }
        [Key]
        public int UserID { get; set; }
        [Key]
        public int? UploadID { get; set; }

        public Category Category = Category.Book;

        public string Title { get; set; }

        public string Authors { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        public string Volume { get; set; }

        public string Pages { get; set; }

        public string Publisher { get; set; }

        [Display(Name = "Key Words")]
        public string KeyWords { get; set; }

        public string Abstract { get; set; }
    }

    public class ThesisVM
    {
        [Key]
        public int PublicationID { get; set; }
        [Key]
        public int UserID { get; set; }
        [Key]
        public int? UploadID { get; set; }

        public Category Category = Category.Thesis;

        public string Title { get; set; }

        public string Authors { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        public string Institution { get; set; }

        [Display(Name = "Key Words")]
        public string KeyWords { get; set; }

        public string Abstract { get; set; }
    }

    public class PatentVM
    {
        [Key]
        public int PublicationID { get; set; }
        [Key]
        public int UserID { get; set; }
        [Key]
        public int? UploadID { get; set; }

        public Category Category = Category.Patent;


        [Display(Name = "Inventors")]
        public string Authors { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Patent Office")]
        public string PatentOffice { get; set; }

        [Display(Name = "Patent Number")]
        public string PatentNumber { get; set; }

        [Display(Name = "Application Number")]
        public string ApplicationNumber { get; set; }

        public string Pages { get; set; }

        public string Publisher { get; set; }

        [Display(Name = "Key Words")]
        public string KeyWords { get; set; }

        public string Abstract { get; set; }
    }

    public class OtherVM
    {
        [Key]
        public int PublicationID { get; set; }
        [Key]
        public int UserID { get; set; }
        [Key]
        public int? UploadID { get; set; }

        public Category Category = Category.Other;

        public string Title { get; set; }

        public string Authors { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        public string Source { get; set; }

        [Display(Name = "Report Number")]
        public string ReportNumber { get; set; }

        [Display(Name = "Key Words")]
        public string KeyWords { get; set; }

        public string Abstract { get; set; }
    }*/
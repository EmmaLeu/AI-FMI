using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AI.Models
{
    public class ImageVM
    {
        [Key]
        public int ImageID { get; set; }
        public int UserID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
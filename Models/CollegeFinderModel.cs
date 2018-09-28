using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MvcApplication3.Models;

namespace MvcApplication3.Models
{
    public class CollegeFinderModel
    {
        [Required]
        [Display(Name = "Select your Region")]
        public string region { get; set; }
        [Required]
        [Display(Name = "Enter Branch")]
        public string branch { get; set; }
        [Required]
        [Display(Name = "Enter Category")]
        public string category { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string gender { get; set; }
        [Required]
        [Display(Name = "Enter Rank")]
        public string rank { get; set; }
       
    }
}
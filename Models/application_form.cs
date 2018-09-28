using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication3.Models
{
    public class application_form
    {

        [Required]
        [Display(Name = "First Name")]
        public string fname { get; set; }


        [Display(Name = "Middle Name")]
        public string mname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lname { get; set; }

        [Required]
        [Display(Name = "Father's Name")]
        public string fatname { get; set; }

        [Required]
        [Display(Name = "Mother's Name")]
        public string Maaname { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public string calender { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required]
        [Display(Name = "Religion")]
        public string religion { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string category { get; set; } 
        
        [Required]
        [Display(Name = "Mother Tongue")]
        public string mt { get; set; }

        [Required]
        [Display(Name = "Annual Family Income")]
        public string afi { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string add1 { get; set; }

        [Required]
        [Display(Name = "Address Line 2")]
        public string add2 { get; set; }

        [Required]
        [Display(Name = "Address Line3")]
        public string add3 { get; set; }

        [Required]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [Display(Name = "PIN Code")]
        public string pinCode { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        public string mob { get; set; }



    }

    public interface Application_formRepository
    {

    }

    public class preferenceCollege
    {
        public int mob { get; set; }
    }
    public class appdb :DbContext
    {
        public appdb()
            :base("sqlConnection")
        {}
        public DbSet<application_form> appform { get; set; }
    }

   public class verifyCertificate
   {
       [Display(Name = "Maths")]
       public int maths { get; set; }

       [Display(Name = "Physics")]
       public int physics { get; set; }

       [Display(Name = "Chemistry")]
       public int chemistry { get; set; }

       [Display(Name = "Maths")]
       public int Mathss { get; set; }

       [Display(Name = "Physics")]
       public int Physicss { get; set; }

       [Display(Name = "Chemistry")]
       public int Chemistrys { get; set; }

       [Display(Name = "English")]
       public int English { get; set; }

       
   }
}
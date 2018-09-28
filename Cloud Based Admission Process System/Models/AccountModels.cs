using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Linq;
using System.Web;

namespace MvcApplication3.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class LoginVerifyModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class LoginCandidateVerifyModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class Univdb: DbContext
    {
        public Univdb()
            :base("newConnection")
        { }
        public DbSet<University> Universities { get; set; }
    }

    public class University_RegModel 
    {
        [Required]
        [Display(Name = "University/Board ID")]
        public int getId { get; set; }

        [Required]
        [Display(Name = "University/Board Name")]
        public string getName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "E-mail ID")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Enter Mobile Number")]
        public string mobCode { get; set; }

        

    }
    
    public class verifysmsModel
    {
        [Display(Name = "Enter Verification Codee")]
        public string UserName { get; set; }
    }
    public class RegisterModel
    {

        [Required]
        [EmailAddress(ErrorMessage="Invalid email address")]
        [Display(Name = "E-mail ID")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

       [Required]
        [Display(Name = "Choose Your Security Question")]
        public string choose { get; set; }

       [Required]
       [Display(Name = "Security Answer")]
       public string ans { get; set; }

       [Display(Name = "Confirm Security Answer")]
       [Compare("ans", ErrorMessage = "The Security Answer and confirmation Security Answer do not match.")]
       public string conans { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
 
    public class certiModel
    {
       
        public string fname { get; set; }
        public string lname { get; set; }
        public string mname { get; set; }
        public string seatNo { get; set; }
        [Required]
        [Display(Name = "QR Code Content")]
        public int Maths { get; set; }
        public int Physics { get; set; }
        public int Chemistry { get; set; }
        [Required]
        [Display(Name = "Data")]
        public string data { get; set; }
       
    }

    public class certiDb : DbContext
    {
        public certiDb()
            :base("sqlConnection")
        { }

        public DbSet<certiModel> certiModels { get; set; }
    }

   

    public class District
    {
        public string StateName { get; set; }
        public string DistrictName { get; set; }

        public static IQueryable<District> GetDistrict()
        {
            return new List<District>
            {
                new District { StateName = "Maharashtra", DistrictName = "Aurangabad" },
                new District { StateName = "Maharashtra", DistrictName = "Bandra" },
                new District { StateName = "Maharashtra", DistrictName = "Nagpur" },
                new District { StateName = "Maharashtra", DistrictName = "Pune" },
                new District { StateName = "Maharashtra", DistrictName = "Akola" },
                new District { StateName = "Maharashtra", DistrictName = "Chandrapur" },
                new District { StateName = "Maharashtra", DistrictName = "Jalgaon" },
                new District { StateName = "Maharashtra", DistrictName = "Jalgaon" },
                new District { StateName = "Maharashtra", DistrictName = "Parbhani" },
                new District { StateName = "Maharashtra", DistrictName = "Sholapur" },
                new District { StateName = "Maharashtra", DistrictName = "Thane" },
                new District { StateName = "Maharashtra", DistrictName = "Latur" },
                new District { StateName = "Maharashtra", DistrictName = "Mumbai-City" },
                new District { StateName = "Maharashtra", DistrictName = "Buldhana" },
                new District { StateName = "Maharashtra", DistrictName = "Dhule" },
                new District { StateName = "Maharashtra", DistrictName = "Kolhpur" },
                new District { StateName = "Maharashtra", DistrictName = "Nanded" },
                new District { StateName = "Maharashtra", DistrictName = "Raigad" },
                new District { StateName = "Maharashtra", DistrictName = "Amravati" },
                new District { StateName = "Maharashtra", DistrictName = "Nashik" },
                new District { StateName = "Maharashtra", DistrictName = "Wardha" },
                new District { StateName = "Maharashtra", DistrictName = "Ahmednagar" },
                new District { StateName = "Maharashtra", DistrictName = "Beed" },
                new District { StateName = "Maharashtra", DistrictName = "Bhandara" },
                new District { StateName = "Maharashtra", DistrictName = "Gadchiroli" },
                new District { StateName = "Maharashtra", DistrictName = "Jalna" },
                new District { StateName = "Maharashtra", DistrictName = "Osmanabad" },
                new District { StateName = "Maharashtra", DistrictName = "Ratnagiri" },
                new District { StateName = "Maharashtra", DistrictName = "Sangli" },
                new District { StateName = "Maharashtra", DistrictName = "Satara" },
                new District { StateName = "Maharashtra", DistrictName = "Sindudurg" },
                new District { StateName = "Maharashtra", DistrictName = "Yavatmal" },
                new District { StateName = "Maharashtra", DistrictName = "Nandurbar" },
                new District { StateName = "Maharashtra", DistrictName = "Washim" },
                new District { StateName = "Maharashtra", DistrictName = "Gondia" },
                new District { StateName = "Maharashtra", DistrictName = "Hingoli" },
                new District { StateName = "Gujarat", DistrictName = "Vadodara" },
                new District { StateName = "Gujarat", DistrictName = "Vapi" },
            }.AsQueryable();
        }
    }

    public class Taluka
    {
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }

        public static IQueryable<Taluka> GetTaluka()
        {
            return new List<Taluka>
            {
                new Taluka { DistrictName = "Thane", TalukaName = "Ambarnath" },
                new Taluka { DistrictName = "Thane", TalukaName = "Bhiwandi" },
                new Taluka { DistrictName = "Thane", TalukaName = "Dahanu" },
                new Taluka { DistrictName = "Thane", TalukaName = "Jawhar" },
                new Taluka { DistrictName = "Thane", TalukaName = "Kalyan" },
                new Taluka { DistrictName = "Thane", TalukaName = "Mokhada" },
                new Taluka { DistrictName = "Thane", TalukaName = "Murbad" },
                new Taluka { DistrictName = "Thane", TalukaName = "Palghar" },
                new Taluka { DistrictName = "Thane", TalukaName = "Shahapur" },
                new Taluka { DistrictName = "Thane", TalukaName = "Talasari" },
                new Taluka { DistrictName = "Thane", TalukaName = "Thane" },
                new Taluka { DistrictName = "Thane", TalukaName = "Ulhasnagar" },
                new Taluka { DistrictName = "Thane", TalukaName = "Vada" },
                new Taluka { DistrictName = "Thane", TalukaName = "Vasai" },
                new Taluka { DistrictName = "Thane", TalukaName = "Vikramgad" },
            }.AsQueryable();
        }
    }
    

}

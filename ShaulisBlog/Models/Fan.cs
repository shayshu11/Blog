using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace ShaulisBlog.Models
{
    public enum Gender
    {
        MALE,
        FEMALE
    }

    public class Fan
    {
        [Key]        
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]        
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Seniority")]
        public int Seniority { get; set; }
        
        [ForeignKey("Permission")]
        public int permissionId { get; set; }
        public virtual Permission Permission { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Provide Email", AllowEmptyStrings = false)]
        public string Email { get; set; }
        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
    }
}
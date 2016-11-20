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
        [Required(ErrorMessage = "Please provide First Name", AllowEmptyStrings = false)]
        [RegularExpression("^([a-zA-Z])+$", ErrorMessage = "Only alphabet")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please provide Last Name", AllowEmptyStrings = false)]
        [RegularExpression("^([a-zA-Z])+$", ErrorMessage = "only alphabet")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1/1/1990", "31/12/2100",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [ForeignKey("Permission")]
        public int permissionId { get; set; }
        public virtual Permission Permission { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Provide Email", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address is not valid")]
        public string Email { get; set; }
        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        public string SessionID { get; set; }
    }
}
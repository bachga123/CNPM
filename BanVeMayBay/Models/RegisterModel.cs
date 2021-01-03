using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanVeMayBay.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "This field can't be empty")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email isn't valid. We use this email to send activation code.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        [Display(Name = "Username")]

        public string Username { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        [Display(Name = "Repeat Password")]
        [DataType(DataType.Password)]

        public string RepeatPassword { get; set; }

    }
}
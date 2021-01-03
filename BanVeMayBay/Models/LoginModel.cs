using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BanVeMayBay.Models;
namespace BanVeMayBay.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username can't be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can't be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
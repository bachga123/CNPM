using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanVeMayBay.Areas.Admin.Data
{
    public class AirportManager
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name can not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nation can not be empty")]
        public string Nation { get; set; }
    }
}
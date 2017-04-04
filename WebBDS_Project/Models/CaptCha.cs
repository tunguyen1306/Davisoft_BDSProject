using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    public class CaptCha
    {
        [Required]
       
        public string Captcha { get; set; } 
    }
}
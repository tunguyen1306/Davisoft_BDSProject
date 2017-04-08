using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    public class NewsModel
    {
        public List<bdspicture> ListPicture { get; set; }
        public bdspicture tblPicture { get; set; }
        public bdsnew tblbdsnew { get; set; }
        public List<bdsnew> ListNew { get; set; }
    }
}
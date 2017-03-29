using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    public class GeoModel
    {
       
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int DistId { get; set; }
        public string DistName { get; set; }
        public int WardId { get; set; }
        public string WardName { get; set; }

    }
}
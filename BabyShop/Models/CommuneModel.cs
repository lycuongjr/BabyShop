using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyShop.Models
{
    public class CommuneModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
    }
}
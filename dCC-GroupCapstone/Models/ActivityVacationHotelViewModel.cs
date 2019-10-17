using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dCC_GroupCapstone.Models
{
    public class ActivityVacationHotelViewModel
    {
        public Vacation vacation { get; set; }
        public Activity activity { get; set; }
        public Hotel hotel { get; set; }
    }
}
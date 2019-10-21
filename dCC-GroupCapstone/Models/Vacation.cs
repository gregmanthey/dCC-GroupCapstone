using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dCC_GroupCapstone.Models
{
    public class Vacation
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name of Vacation")]
        public string VacationName { get; set; }
        [Display(Name = "Mark as private?")]
        public bool IsPrivate { get; set; }
        [Display(Name = "Location")]
        public string LocationQueried { get; set; }

        public string LatLong { get; set; }
        public int CustomerCreated { get; set; }
        public double Cost { get; set; }

        [Display(Name = "Rating")]
        public double AverageRating { get; set; }
        public List<Activity> Activities
        {
            get { return _Activities; }
            set { _Activities = value; }
        }

        private List<Activity> _Activities;

        public string ActivitiesSerialized
        {
            get { return JsonConvert.SerializeObject(_Activities); }
            set { _Activities = JsonConvert.DeserializeObject<List<Activity>>(value); }
        }
        public List<Hotel> Hotels
        {
            get { return _Hotels; }
            set { _Hotels = value; }
        }

        private List<Hotel> _Hotels;

        public string HotelsSerialized
        {
            get { return JsonConvert.SerializeObject(_Hotels); }
            set { _Hotels = JsonConvert.DeserializeObject<List<Hotel>>(value); }
        }
    }
}
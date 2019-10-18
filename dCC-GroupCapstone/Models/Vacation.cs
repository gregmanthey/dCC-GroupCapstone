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
        [ForeignKey("Hotel")]
        [Display(Name = "Hotel Name")]
        public int SavedHotel { get; set; }
        public Hotel Hotel { get; set; }
        [Display(Name = "Rating")]
        public double AverageRating { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
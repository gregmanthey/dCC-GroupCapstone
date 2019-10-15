using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dCC_GroupCapstone.Models
{
    public class Rating
    {
        [Key]
        [Column(Order = 0)]
        public int CustomerId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int VacationId { get; set; }
        public double RatingValue { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("VacationId")]
        public virtual Vacation Vacation { get; set; }
    }
}
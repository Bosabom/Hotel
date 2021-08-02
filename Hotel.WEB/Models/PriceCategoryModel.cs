using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class PriceCategoryModel
    {
        
        [DisplayName("ID")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Price per night (in $)")]
        public double Price { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [DisplayName("Category Id")]
        public int CategoryId { get; set; }

    }
}
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
        [Range(typeof(double),"100","1000",ErrorMessage="Data isn't in range (100, 1000)")]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("Start Date")]
        public string StartDate_short
        {
            get
            {
                return StartDate.ToShortDateString();
            }
        }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DisplayName("End Date")]
        public string EndDate_short
        {
            get
            {
                return EndDate.ToShortDateString();
            }
        }

        [Required]
        [DisplayName("Category Id")]
        public int CategoryId { get; set; }

        public override string ToString()
        {
            return $"Category ID = {CategoryId}; Price = {Price}; Start Date = {StartDate}; End Date = {EndDate}";
        }

    }
}
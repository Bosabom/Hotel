using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DAL.Entities
{
    public class PriceCategory
    {
        [Key]
        public int Id { get; set; }

        public double Price { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //ForeignKey на Category
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}

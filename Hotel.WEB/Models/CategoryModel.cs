using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class CategoryModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]

        public string Name { get; set; }

        [Required]
        [DisplayName("Number of Places")]
        public int Number_Of_Places { get; set; }
    }
}
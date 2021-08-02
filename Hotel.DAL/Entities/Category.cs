using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DAL.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number_Of_Places { get; set; }
    }
}

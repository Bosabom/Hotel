using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Room Title")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Active Status")]

        public bool Active { get; set; }
        [Required]
        [DisplayName("Category ID")]

        public int CategoryId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DAL.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Active { get; set; }//если комната активная или перестает существовать

        //CategoryId - FK на таблицу категорий
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category RoomCategory { get; set; }
    }
}

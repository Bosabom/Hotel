using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public DateTime LogDate { get; set; }

        public string User { get; set; }

        public string Action { get; set; }

        public string Entity { get; set; }

        public int EntityId { get; set; }

        public string Details { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class LogDTO
    {
        public DateTime LogDate { get; set; }

        public string User { get; set; }

        public string Action { get; set; }

        public string Entity { get; set; }

        public int EntityId { get; set; }

        public string Details { get; set; }
    }
}
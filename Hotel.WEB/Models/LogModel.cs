using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class LogModel
    {
        [DisplayName("Log date and time")]
        public DateTime LogDate { get; set; }
        [DisplayName("User name")]
        public string User { get; set; }
        
        public string Action { get; set; }

        public string Entity { get; set; }
        
        [DisplayName("ID")]

        public int EntityId { get; set; }
        [DisplayName("Detailed information")]

        public string Details { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }//если комната активная или перестает существовать
       
        public int CategoryId { get; set; }
        public CategoryDTO RoomCategory { get; set; }

    }
}

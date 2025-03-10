﻿using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface ILogService
    {
        IEnumerable<LogDTO> GetAll();

        void Create(LogDTO logDTO);
    }
}
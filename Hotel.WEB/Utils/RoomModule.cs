﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Ninject.Modules;

namespace Hotel.WEB.Utils
{
    public class RoomModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRoomService>().To<RoomService>();
        }
    }
}
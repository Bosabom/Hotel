﻿using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Utils
{
    public class GuestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGuestService>().To<GuestService>();
        }
    }
}
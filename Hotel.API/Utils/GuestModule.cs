using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;

namespace Hotel.API.Utils
{
    public class GuestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGuestService>().To<GuestService>();
        }
    }
}
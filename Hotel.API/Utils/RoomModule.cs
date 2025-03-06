using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;

namespace Hotel.API.Utils
{
    public class RoomModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRoomService>().To<RoomService>();
        }
    }
}
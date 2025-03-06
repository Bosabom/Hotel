using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;

namespace Hotel.WEB.Utils
{
    public class LogModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogService>().To<LogService>();
        }
    }
}
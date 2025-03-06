using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;

namespace Hotel.WEB.Utils
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
        }
    }
}
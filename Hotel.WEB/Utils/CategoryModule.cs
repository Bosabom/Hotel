﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Ninject.Modules;

namespace Hotel.WEB.Utils
{
    public class CategoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>();
        }
    }
   
}
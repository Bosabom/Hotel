﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Repositories;

namespace Hotel.BLL.Infrastructure
{
    public class DependencyModule : NinjectModule
    {
        private string connectionString;

        public DependencyModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IWorkUnit>().To<EFWorkUnit>().WithConstructorArgument(connectionString);
        }
    }
}
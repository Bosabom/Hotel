using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Ninject.Web.WebApi.Filter;
using Hotel.API.Utils;
using Hotel.BLL.Infrastructure;

namespace Hotel.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule dependencyModule = new DependencyModule("HotelModel");

            //Guest
            NinjectModule guestModule = new GuestModule();

            //Room
            NinjectModule roomModule = new RoomModule();

            //Booking
            NinjectModule bookingModule = new BookingModule();

            //Category
            NinjectModule categoryModule = new CategoryModule();

            //PriceCategory
            NinjectModule pricecategoryModule = new PriceCategoryModule();

            var kernel = new StandardKernel(guestModule, roomModule, bookingModule, categoryModule, pricecategoryModule, dependencyModule);
            kernel.Bind<DefaultFilterProviders>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());
            kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));
            GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
        }
    }
}
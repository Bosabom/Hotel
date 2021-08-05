using Hotel.BLL.Infrastructure;
using Hotel.WEB.Utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hotel.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
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

            //User
            NinjectModule userModule = new UserModule();

            //Log
            NinjectModule logModule = new LogModule();

            var kernel = new StandardKernel(guestModule, roomModule, bookingModule, categoryModule, 
                                            pricecategoryModule,userModule,logModule,dependencyModule);
           
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));
        }
    }
}

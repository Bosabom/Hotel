using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.WEB.Models;
namespace Hotel.WEB.Controllers
{
    public class UserController : Controller
    {

        IUserService service;
        IMapper mapper;
        public UserController(IUserService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<UserModel, UserDTO>()).CreateMapper();
        }
        // GET: User
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user_model)
        {
            if (ModelState.IsValid)
            {
               
             var new_user = service.Get(mapper.Map<UserModel, UserDTO>(user_model));

                if (new_user != null)
                {
                    FormsAuthentication.SetAuthCookie(user_model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User not found...");
                }

            }
            return View(user_model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUserModel registerUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserModel userModel = new UserModel { Login = registerUser.Login, Password = registerUser.Password };

                    service.Create(mapper.Map<UserModel, UserDTO>(userModel));
                    return RedirectToAction("Login");
                }
                catch
                {
                    ModelState.AddModelError("", "User with this login already exists!");

                }
            }
            return View();
        }
    }
}

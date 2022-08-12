using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataLayer.Dal;
using DataLayer.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RWA.Models;

namespace RWA.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AspNetUsers user = RepoFactory.GetRepo().AuthUser(model.Email, model.Password);


            if (user == null)
            {
                ViewBag.ErrorMessage = "Wrong email or password!";
                return View(model);
            }

            Session["user"] = user;

            return RedirectToAction("ShowAllApartments", "Home");
        }

        //


        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            RepoFactory.GetRepo().RegisterUser(new DataLayer.Model.AspNetUsers {
                Email = model.Email,
                PasswordHash = Cryptography.HashPassword(model.Password),
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Address = model.Address
            });

            return View("Login");
        }

        public ActionResult Odjava()
        {
            Session["user"] = null;

            return RedirectToAction("ShowAllApartments", "Home");
        }
    }
}